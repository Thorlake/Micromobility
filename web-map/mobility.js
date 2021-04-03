
import { ScooterMap } from './scooter/scooter-map';
import { ScooterService } from './scooter/scooter.service';

export class Mobility {
    #form;
    #scooterMap = new ScooterMap();
    #scooterService = new ScooterService();

    init() {
        this.#scooterMap.build('map');
        this.#form = document.getElementById("scooterForm");
        this.#subscribeToUdateMapSourceOnFindButtonClick();
        this.#subscribeToUpdateCoordinatesOnMapClick();
    }

    #subscribeToUdateMapSourceOnFindButtonClick() {
        this.#form.getElementsByTagName('button')[0].addEventListener("click", () => {
            const scooterLocations$ = this.#getScooterLocations();
            scooterLocations$.then(scooterLocations => {
                this.#scooterMap.updateSource(scooterLocations)
            })
        });
    }

    #subscribeToUpdateCoordinatesOnMapClick() {
        this.#scooterMap.subscribeToMapClick((longitude, latitude) => {
            this.#form.elements.namedItem('landmarkLongitude').value = longitude;
            this.#form.elements.namedItem('landmarkLatitude').value = latitude;
        })
    }

    #getScooterLocations() {
        const numbers = this.#form.elements.namedItem('numberOfScooters').value;
        const searchRadiusMeters = this.#form.elements.namedItem('searchRadiusMeters').value;
        const landmarkLongitude = this.#form.elements.namedItem('landmarkLongitude').value;
        const landmarkLatitude = this.#form.elements.namedItem('landmarkLatitude').value;

        return this.#scooterService.getListBy(
            numbers,
            searchRadiusMeters,
            landmarkLongitude,
            landmarkLatitude
        );
    }

}
