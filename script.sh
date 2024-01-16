#!/bin/bash

#Dit script werkt alleen als je met Git Bash in de bovenste laag van de file structure (./WDPR_HER) het volgende typt: ./script.sh (en dan op enter drukt).
# Change directory and run npm
cd wdpr_her
npm run start &
echo "Frontend is running" &

# Change directory and run dotnet
cd backend/api 
dotnet run
echo "Dotnet is running"

wait

echo "Both the frontend and backend are running"