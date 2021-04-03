import 'ol/ol.css';
import Feature from 'ol/Feature';
import Map from 'ol/Map';
import Point from 'ol/geom/Point';
import View from 'ol/View';
import {
    Circle as CircleStyle,
    Fill,
    Stroke,
    Style,
    Text,
} from 'ol/style';
import { Cluster, OSM, Vector as VectorSource } from 'ol/source';
import { Tile as TileLayer, Vector as VectorLayer } from 'ol/layer';
import { fromLonLat, toLonLat } from 'ol/proj';

export class ScooterMap {
    #map;
    #companyCoordinates = fromLonLat([103.834306, 1.303056]);
    #scootersSource;

    build(id) {
        const layers = this.#getLayers();
        this.#map = new Map({
            layers: layers,
            target: id,
            view: new View({
                center: this.#companyCoordinates,
                zoom: 18,
            }),
        });
    }

    subscribeToMapClick(callback) {
        this.#map.on('singleclick', function (event) {
            const coordinate = toLonLat(event.coordinate);
            callback(coordinate[0], coordinate[1]);
        });
    }

    updateSource(scooterLocations) {
        this.#scootersSource.clear();
        scooterLocations.forEach(scooterLocation => {
            const coordinate = fromLonLat([scooterLocation.longitude, scooterLocation.latitude]);
            const feature = new Feature(new Point(coordinate));
            this.#scootersSource.addFeature(feature);
        });
    }

    #getLayers() {
        this.#scootersSource = new VectorSource();
        const scootersCluster = new VectorLayer({
            source: new Cluster({
                distance: 40,
                source: this.#scootersSource,
            })
        });
        const mapStyleByFeatureSize = new global.Map();
        scootersCluster.setStyle((feature) => {
            const size = feature.get('features').length;
            let style = mapStyleByFeatureSize[size];
            if (!style) {
                style = this.#getStyle(size);
                mapStyleByFeatureSize[size] = style;
            }
            return style;
        })

        const raster = new TileLayer({
            source: new OSM(),
        });

        return [raster, scootersCluster];
    }

    #getStyle(size) {
        const shapeStrokeColor = '#000';
        const shapeFillColor = '#7d00e6';
        const textFillColor = '#fff';

        return new Style({
            image: new CircleStyle({
                radius: 10,
                stroke: new Stroke({
                    color: shapeStrokeColor,
                }),
                fill: new Fill({
                    color: shapeFillColor,
                }),
            }),
            text: new Text({
                text: size.toString(),
                fill: new Fill({
                    color: textFillColor,
                }),
            }),
        });
    }
}
