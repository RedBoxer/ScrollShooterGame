var express = require("express");
var bodyparser = require("body-parser");
var fs = require("fs");
var jsonParser = bodyparser.json();
var app = express();
app.use(express.json());

app.get("/check", (req, res) => {
    
     res.send("Connection online");
});

app.get("/getSave/:id", (req, res) => {
    console.log(req.params["id"] + ".json")
    var data;
    fs.readFile(req.params["id"] + ".json", 'utf-8', function (err, data) {
        if (err) throw err;
        data = JSON.parse(data);
        res.json(data);
    });
    
});

app.post("/sendUser/:id", jsonParser, (req, res) => {
    var user = req.body;
    console.log(user);
    fs.writeFile(req.params["id"] + ".json", JSON.stringify(user), function (err, data){if (err) throw err;});
    res.send();
});

app.listen(3000, () => {
    console.log("Server started");
});
