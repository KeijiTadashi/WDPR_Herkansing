import React from "react";
import useLocalStorage from "use-local-storage";
import Header from "../standaardformats/Header";
import "../CSS/StichtingTheme.css";
import "../CSS/homepage.css";

export const Homepage = () => {
  const defaultDark = window.matchMedia("(prefers-color-scheme: dark)").matches;

  const [theme] = useLocalStorage("theme", defaultDark ? "dark" : "light");
  const [fontSize] = useLocalStorage("font-size", "normal");

  return (
    <>
      
      <main className="Main" data-theme={theme} data-font-size={fontSize}>
        <Header Title={"Stichting Accessibility"} />
        <div class="intro">
          <h2>
            Welkom bij Stichting Accessibility - Toegankelijkheid voor Iedereen
          </h2>
          <p>
            Bij Accessibility geloven we dat iedereen, ongeacht zijn of haar
            beperking, recht heeft op volledige toegang tot de digitale wereld.
            Ons doel is om barrières te doorbreken en gelijke kansen te creëren,
            zodat iedereen kan genieten van de voordelen van technologie.
          </p>
        </div>

        <div class="header">
          <h3>Onze expertises op een rij</h3>
        </div>
        <div class="container">
          <div class="expertise-card">
            <h3>Toegankelijke digitale omgeving</h3>
            <p>
              Onze website is toegewijd aan het ondersteunen van mensen met
              beperkingen bij hun online ervaring. We streven ernaar om een
              inclusieve digitale omgeving te bieden waar alle gebruikers
              gemakkelijk toegang hebben tot informatie en diensten, ongeacht
              hun beperkingen. We voldoen aan strenge toegankelijkheidsnormen en
              gebruiken bruikbaarheidshulpmiddelen om een positieve online
              ervaring voor iedereen te garanderen.
            </p>
          </div>
          <div class="expertise-card">
            <h3>Toegankelijke fysieke omgeving</h3>
            <p>
              We streven naar een toegankelijke fysieke omgeving, waar alle
              ruimtes binnen en rondom het gebouw drempelvrij, goed verlicht en
              gemarkeerd zijn met geleidelijnen en kleurcontrasten. Dit zorgt
              voor een inclusieve en veilige ruimte, waar iedereen zich vrij kan
              bewegen, ongeacht hun mobiliteitsbeperkingen.
            </p>
          </div>
          <div class="expertise-card">
            <h3>Gebruiksvriendelijke omgeving</h3>
            <p>
              Verbeter de gebruiksvriendelijkheid van de omgeving door samen te
              werken met ervaringsdeskundigen. Deze samenwerking vergroot de
              toegankelijkheid en bruikbaarheid, waardoor de omgeving voor een
              breder publiek geschikt wordt. We streven naar een inclusieve
              benadering die rekening houdt met diverse perspectieven en
              behoeften.
            </p>
          </div>
        </div>
      </main>
    </>
  );
};

export default Homepage;
