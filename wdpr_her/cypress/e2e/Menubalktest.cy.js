describe('Is de website online?', () => {

  beforeEach(()=>{
    cy.visit('localhost:3000');
  });

  it('Visits the localhost', () => {

    //Controleer of de menubalk bestaat
    cy.get('ul#menubalk').within( ()=>{
      //Vervolgens checken of er exact 3 items in staan
      cy.get('li').should('have.length', 3);
    });
  })

   
})