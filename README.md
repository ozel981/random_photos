# Random photos

## Opis

Aplikacji typu JSON API do pobierania losowych zdjęć z serwisu internetowego Reddit. API posiada 2 endpointy:
1. `/random` - pobiera losowy obrazek z Reddita dla zadanego subreddita
2. `/history` - pobiera historię wylosowanych obrazków.
Subreddit jest konfigurowalny i znajduje się w pliku appsettings.json.
Zdjęcia zwracane są w postaci JSON:
{
    "url": "https://photo.jpg",
    "downloadDate": "2022-04-03T23:50:55.854025"
},

## Uruchomienie



## Autor

Wojciech Podmokły (https://github.com/ozel981)
