#!/bin/bash

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