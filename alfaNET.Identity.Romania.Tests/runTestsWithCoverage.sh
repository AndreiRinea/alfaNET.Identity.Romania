#!/bin/bash

# dotnet tool install -g dotnet-reportgenerator-globaltool

dotnet test --collect:"XPlat Code Coverage"

reportgenerator \
  -reports:"**/coverage.cobertura.xml" \
  -targetdir:"coverage-report" \
  -reporttypes:Html
  
open coverage-report/index.html
