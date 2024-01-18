describe('Login Functionality: login succeeds', () => {
    beforeEach(() => {
        // Visit the login page before each test
        cy.visit('http://localhost:3000/Login');
    });
    it('should receive an OK and logs in', () => {

        const validUsername = 'Testlkjg';
        const validPassword = 'Wachtwoord1!'

        // vul login form in
        cy.get('#username').type(validUsername);
        cy.get('#password').type(validPassword);
        cy.get('button[aria-label="Log in"]').click();

        cy.intercept('POST', 'http://localhost:5027/Login', {
            statusCode: 200,
        }).as('loginRequest2');
        // Wait for the intercepted API call to complete
        cy.wait('@loginRequest2', { timeout: 15000 }).then((interception) => {
            // Assertions on the intercepted request and response for a successful login
            expect(interception.request.method).to.equal('POST');
            expect(interception.response.statusCode).to.equal(200);
        });
        
        
        
    });
});