# Coding Challenge

## Backend

Das Backend wurde als MVC-WebApi realisiert. Es werden keine Views verwendet.

Beispieldaten sind in einer SQLite Datenbank hinterlegt und können über einen REST-Endpunkt abgefragt werden. Die relevanten Wetterdaten werden im gleichen Format wie die Daten von OpenWeatherMap ausgegeben. Für die Abfrage wird der Städtename als URL-Parameter genutzt. Koordinaten können nicht verwendet werden.

Liste der Städtenamen in der DB:

- Erfurt
- London
- Paris
- Dresden
- Tokio
- Delhi
- Shanghai
- Sao Paulo
- Kairo
- New York

## Frontend

Das Frontend wurde mit HTML, CSS und Javascript realisiert. Für die Abfrage der Wetterdaten wird jQuery genutzt.

Das Frontend kann sowohl Daten von OpenWeatherMap als auch Daten aus dem Backend anzeigen. Mit Hilfe von **useOpenWeatherMap** in der Javascript-Datei kann zwischen beiden Quellen gewechselt werden. Die Url für das Backend muss gegebenenfalls angepasst werden. Für OpenWeatherMap muss die **appid** angegeben werden.

Falls durch den Browser Koordinaten bereitgestellt werden, wird die zugehörige Stadt mit Hilfe der OpenWeatherMap Api ermittelt und die Wetterdaten werden angezeigt.
