@allowed([
  'dev'
  'test'
  'prod'
])
param environment string
param ghcrUsername string

@secure()
param ghcrPassword string

module containerAppsEnvironment 'containerAppsEnvironment.bicep' = {
  name: 'containerAppsEnvironment-containerApps'
  params: {
    environment: environment
  }
}

module keyVault 'keyVault.bicep' = {
  name: 'keyVault-containerApps'
  params: {
    environment: environment
  }
}

output containerAppsEnvironmentId string = containerAppsEnvironment.outputs.containerAppsEnvironmentId

module api 'containerApps/api.bicep' = {
   params: {
    containerAppsEnvironmentId: containerAppsEnvironment.outputs.containerAppsEnvironmentId
    ghcrUsername: ghcrUsername
    ghcrPassword: ghcrPassword
    initKeyVaultSecretsOnly: true
    keyVaultUri: keyVault.outputs.keyVaultUri
    keyVaultName: keyVault.outputs.keyVaultName
    tagNumber: '1'
  }
}

module webApp 'containerApps/webApp.bicep' = {
   params: {
    containerAppsEnvironmentId: containerAppsEnvironment.outputs.containerAppsEnvironmentId
    environment: environment
    ghcrUsername: ghcrUsername
    ghcrPassword: ghcrPassword
    initKeyVaultSecretsOnly: true
    keyVaultUri: keyVault.outputs.keyVaultUri
    keyVaultName: keyVault.outputs.keyVaultName
    tagNumber: '1'
  }
}
