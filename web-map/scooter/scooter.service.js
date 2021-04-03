

export class ScooterService {
    #baseUrl = 'https://localhost:44349';
    #apiUrl = `${this.#baseUrl}/scooters`;

    getListBy(
        numbers,
        searchRadiusMeters,
        landmarkLongitude,
        landmarkLatitude
    ) {
        const url = new URL(this.#apiUrl);
        const params = {
            numbers: numbers,
            searchRadiusMeters: searchRadiusMeters,
            landmarkLongitude: landmarkLongitude,
            landmarkLatitude: landmarkLatitude
        };
        Object.keys(params).forEach(key => url.searchParams.append(key, params[key]))
        return fetch(url)
            .then(response => response.json())
            .then(response => response.locations);
    }
}
