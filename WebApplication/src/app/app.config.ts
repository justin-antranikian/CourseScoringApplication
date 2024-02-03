import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { importProvidersFrom } from '@angular/core';
import { routes } from './app.routes';
import { provideClientHydration } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';

export const appConfig: ApplicationConfig = {
  // imports: [CommonModule],
  providers: [provideRouter(routes), provideClientHydration(), importProvidersFrom(HttpClientModule)]
};
