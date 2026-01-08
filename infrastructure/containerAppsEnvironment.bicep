
var location string = resourceGroup().location
param environment string

resource containerAppsEnvironment 'Microsoft.App/managedEnvironments@2023-05-01' = {
  name: 'container-apps-environment-${environment}'
  location: location
  properties: {}
}

output containerAppsEnvironmentId string = containerAppsEnvironment.id
