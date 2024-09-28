#### Request Formatı
```json
{
    "Tren":
    {
        "Ad":"Ada Ekspresi",
        "Vagonlar":
        [
            {"Ad":"Vagon 1", "Kapasite":100, "DoluKoltukAdet":68},
            {"Ad":"Vagon 2", "Kapasite":90, "DoluKoltukAdet":50},
            {"Ad":"Vagon 3", "Kapasite":80, "DoluKoltukAdet":80}
        ]
    },
    "RezervasyonYapilacakKisiSayisi":3,
    "KisilerFarkliVagonlaraYerlestirilebilir":true
}
```

#### Response Formatı
```json
{
    "RezervasyonYapilabilir":true,
    "YerlesimAyrinti":
    [
        {"VagonAdi":"Vagon 1","KisiSayisi":2},
        {"VagonAdi":"Vagon 2","KisiSayisi":1}
    ]
}
```
