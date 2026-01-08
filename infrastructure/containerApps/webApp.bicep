
var location string = resourceGroup().location

param containerAppsEnvironmentId string
param environment string
param ghcrUsername string

@secure()
param ghcrPassword string
param initKeyVaultSecretsOnly bool
param keyVaultUri string
param keyVaultName string
param tagNumber string

var containerAppName string = 'admin-app'

module config '../environmentVariables.bicep' = {
  name: '${containerAppName}-config'
}

var keyVaultSecrets array = initKeyVaultSecretsOnly ? [] : []

resource containerApp 'Microsoft.App/containerApps@2025-07-01' = {
  name: containerAppName
  location: location
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    environmentId: containerAppsEnvironmentId
    configuration: {
      ingress: {
        external: true
        targetPort: 3000
      }
      registries: [
        {
          server: 'ghcr.io'
          username: ghcrUsername
          passwordSecretRef: 'ghcr-password'
        }
      ]
      secrets: [
        ...map(keyVaultSecrets, (secret) => {
          name: secret.name
          keyVaultUrl: '${keyVaultUri}secrets/${secret.name}'
          identity: 'system'
        })
        {
          name: 'ghcr-password'
          value: ghcrPassword
        }
      ]
    }
    template: {
      containers: [
        {
          name: containerAppName
          image: 'ghcr.io/justin-antranikian/${containerAppName}:${tagNumber}'
          resources: {
            cpu: json('0.25')
            memory: '0.5Gi'
          }
          env: [
            ...map(keyVaultSecrets, (secret) => {
              name: secret.environmentVariableName
              secretRef: secret.name
            })
            {
              name: 'API_HOST'
              value: config.outputs.apiHost[environment]
            }
          ]
        }
      ]
      scale: {
        minReplicas: 1
        maxReplicas: 1
      }
    }
  }
}

resource keyVault 'Microsoft.KeyVault/vaults@2023-07-01' existing = {
  name: keyVaultName
}

resource rbacAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(containerApp.id, 'KeyVaultSecretsUser')
  scope: keyVault
  properties: {
    roleDefinitionId: subscriptionResourceId('Microsoft.Authorization/roleDefinitions', '4633458b-17de-408a-b874-0445c86b69e6') // Key Vault Secrets User
    principalId: containerApp.identity.principalId
    principalType: 'ServicePrincipal'
  }
}
