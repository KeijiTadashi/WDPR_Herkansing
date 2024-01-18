// cypress/integration/login.spec.js

describe('Login Functionality: login fails', () => {
    beforeEach(() => {
        // Visit the login page before each test
        cy.visit('http://localhost:3000/Login');
    });


    it('should receive error 404', () => {

        const invalidUsername = 'MarcoHietala';
        const invalidPassword = 'Nightwish';

        cy.intercept('POST', 'http://localhost:5027/Login', {
            statusCode: 404,
            body: {
                code: '404',
                details: 'Error: Not Found',
                message: 'De gebruikersnaam bestaat niet.'
            }
        }).as('loginRequest');

        cy.get('#username').type(invalidUsername);
        cy.get('#password').type(invalidPassword);
        cy.get('button[aria-label="Log in"]').click();

        cy.wait('@loginRequest').then((interception) => {
            expect(interception.response.statusCode).to.equal(404);
            expect(interception.response.body).to.deep.equal({
                code: '404',
                details: 'Error: Not Found',
                message: 'De gebruikersnaam bestaat niet.'
            });
        });
    });
});
