@allowed([
  'dev'
  'test'
  'prod'
])
param environment string

@secure()
param sqlServerLogin string
@secure()
param sqlServerPassword string

module sqlServer './sqlServer.bicep' = {
  name: 'sqlServer'
  params: {
    environment: environment
    sqlServerLogin: sqlServerLogin
    sqlServerPassword: sqlServerPassword
  }
}

module keyVault 'keyVault.bicep' = {
  name: 'keyVault-main'
  params: {
    environment: environment
  }
}

module containerAppsEnvironment './containerAppsEnvironment.bicep' = {
  name: 'containerAppsEnvironment'
  params: {
    environment: environment
  }
}

output containerAppsEnvironmentId string = containerAppsEnvironment.outputs.containerAppsEnvironmentId
