# Mobility

Find nearest scooters on a map within a certain distance

## Stack
.NET Core 3.1 - backend  
JavaScript + OpenLayers - frontend

## Algorithm 
- Scooters are randomly generated (triangulation) in a city (2D/polygon)
- Nearest scooters are searched using great distance calculation (3D/sphere)

## Usage

### Core server
- Run `dotnet run`

### Web app
- Run `npm install` 
- Run `npm start`

## Interal options

### Core server
- Provide `ICityMapService` implementation to change a city map
- Change `_scooterNumbersToGenerate` in `ScooterSeeder.cs` to generate a certain amount of scooters  
