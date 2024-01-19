import React, {useState} from "react";
import OpdrachtPaneel from "./OpdrachtPaneel";

const DynamicOpdrachtPaneel = ({opdrachtArray}) => {
    const [opdrachtCounter] = useState(1);

    /*
    Hoe werkt DynamicOpdrachtPaneel?
    Je geeft een Array mee met Objecten die allemaal elk kunnen fucntioneren als OpdrachtPaneel.
    const opdrachtArray = [
          { naam: "If it hadn't been for", aantaldeelnemers: 69 }];
          return(
            <DynamicOpdrachtPaneel opdrachtArray={opdrachtArray} />
          )
          Dan maakt die x Opdrachtpanelen onder elkaar, waar X het aantal objecten in opdrachtArray is.
          Ze worden allemaal automatisch geindexeerd.
    */
    return (
        <>
            {opdrachtArray.map((opdracht, index) => (
                <OpdrachtPaneel
                    key={index}
                    {...opdracht}
                    indexnr={opdrachtCounter + index}  /* Pass the correct index */
                />
            ))}
        </>
    );
};

export default DynamicOpdrachtPaneel;
