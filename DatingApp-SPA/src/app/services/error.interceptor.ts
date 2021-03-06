import {
  HttpInterceptor,
  HttpErrorResponse,
  HTTP_INTERCEPTORS,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  intercept(
    req: import('@angular/common/http').HttpRequest<any>,
    next: import('@angular/common/http').HttpHandler
  ): import('rxjs').Observable<import('@angular/common/http').HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((error) => {
        if (error.status === 401) {
          return throwError(error.statusText);
        }

        if (error instanceof HttpErrorResponse) {
          // Application-Error- We set in  NET app using Extension method. It mainly deals with 500.
          const applicationError = error.headers.get('Application-Error');
          if (applicationError) {
            return throwError(applicationError);
          }
        }

        // For any model errors like user name required, password length etc..
        // So the errors are in error.error [Check in the Console]
        const serverError = error.error;
        let modelStateErrors = '';
        if (serverError.errors && typeof serverError.errors === 'object') {
          for (const key in serverError.errors) {
            if (serverError.errors[key]) {
              modelStateErrors += serverError.errors[key] + '\n';
            }
          }
        }

        return throwError(modelStateErrors || serverError || 'Server Error');
      })
    );
  }
}

export const ErrorInterceptorProvider = {
  provide: HTTP_INTERCEPTORS,
  useClass: ErrorInterceptor,
  multi: true,
};
