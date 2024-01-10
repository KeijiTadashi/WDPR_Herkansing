// DynamicOpdrachtPaneel.js

import React, { useState } from "react";
import OpdrachtPaneel from "../standaardformats/OpdrachtPaneel";

const DynamicOpdrachtPaneel = ({ opdrachtArray }) => {
  const [opdrachtCounter, setOpdrachtCounter] = useState(1);

  const handleAddOpdracht = () => {
    setOpdrachtCounter((prevCounter) => prevCounter + 1);
  };

  return (
    <>
      {opdrachtArray.map((opdracht, index) => (
        <OpdrachtPaneel
          key={index}
          {...opdracht}
          indexnr={opdrachtCounter + index}  /* Pass the correct index */
        />
      ))}
      <button onClick={handleAddOpdracht}>Add Opdracht</button>
    </>
  );
};

export default DynamicOpdrachtPaneel;
