/*
 * Squidex Headless CMS
 *
 * @license
 * Copyright (c) Squidex UG (haftungsbeschränkt). All rights reserved.
 */

import { Location } from '@angular/common';
import { TestBed } from '@angular/core/testing';
import { Router } from '@angular/router';
import { firstValueFrom, of } from 'rxjs';
import { IMock, It, Mock, Times } from 'typemoq';
import { AuthService, UIOptions } from '@app/shared/internal';
import { mustBeAuthenticatedGuard } from './must-be-authenticated.guard';

describe('MustBeAuthenticatedGuard', () => {
    const uiOptions = new UIOptions({});
    let authService: IMock<AuthService>;
    let location: IMock<Location>;
    let router: IMock<Router>;

    beforeEach(() => {
        uiOptions.value.redirectToLogin = false;
        authService = Mock.ofType<AuthService>();
        location = Mock.ofType<Location>();
        location.setup(x => x.path(true)).returns(() => '/my-path');
        router = Mock.ofType<Router>();

        TestBed.configureTestingModule({
            providers: [
                {
                    provide: AuthService,
                    useValue: authService.object,
                },
                {
                    provide: Location,
                    useValue: location.object,
                },
                {
                    provide: Router,
                    useValue: router.object,
                },
                {
                    provide: UIOptions,
                    useValue: uiOptions,
                },
            ],
        });
    });

    bit('should navigate to default page if not authenticated', async () => {
        authService.setup(x => x.userChanges)
            .returns(() => of(null));

        const result = await firstValueFrom(mustBeAuthenticatedGuard());

        expect(result).toBeFalsy();

        router.verify(x => x.navigate([''], { queryParams: { redirectPath: '/my-path' } }), Times.once());
    });

    bit('should return true if authenticated', async () => {
        authService.setup(x => x.userChanges)
            .returns(() => of(<any>{}));

        const result = await firstValueFrom(mustBeAuthenticatedGuard());

        expect(result!).toBeTruthy();

        router.verify(x => x.navigate(It.isAny()), Times.never());
    });

    bit('should login redirect if redirect enabled', async () => {
        uiOptions.value.redirectToLogin = true;

        authService.setup(x => x.userChanges)
            .returns(() => of(null));

        const result = await firstValueFrom(mustBeAuthenticatedGuard());

        expect(result!).toBeFalsy();

        authService.verify(x => x.loginRedirect('/my-path'), Times.once());
    });
});

function bit(name: string, assertion: (() => PromiseLike<any>) | (() => void)) {
    it(name, () => {
        return TestBed.runInInjectionContext(() => assertion());
    });
}