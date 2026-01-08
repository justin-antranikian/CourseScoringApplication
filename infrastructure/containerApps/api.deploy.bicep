@allowed([
  'dev'
  'test'
  'prod'
])
param environment string
param ghcrUsername string

@secure()
param ghcrPassword string
param tagNumber string

var containerAppName string = 'admin-app'

module containerAppsEnvironment '../containerAppsEnvironment.bicep' = {
  name: 'containerAppsEnvironment-${containerAppName}'
  params: {
    environment: environment
  }
}

module keyVault '../keyVault.bicep' = {
  name: 'keyVault-${containerAppName}'
  params: {
    environment: environment
  }
}

output containerAppsEnvironmentId string = containerAppsEnvironment.outputs.containerAppsEnvironmentId

module api './api.bicep' = {
   params: {
    containerAppsEnvironmentId: containerAppsEnvironment.outputs.containerAppsEnvironmentId
    ghcrUsername: ghcrUsername
    ghcrPassword: ghcrPassword
    initKeyVaultSecretsOnly: false
    keyVaultUri: keyVault.outputs.keyVaultUri
    keyVaultName: keyVault.outputs.keyVaultName
    tagNumber: tagNumber
  }
}
