const bodyParser = require('body-parser');
const mongoose = require('mongoose')
const mongoString = process.env.DATABASE_URL;

mongoose.connect(mongoString);
const database = mongoose.connection;

database.on('error', (error) => {
    database.collection("player");
    console.log(error)
})

database.once('connected', () => {
    console.log('Database Connected');
})

const express = require('express');
const app = express();

app.use(bodyParser.json());
app.use(
  bodyParser.urlencoded({
    extended: true,
  }),
);

app.get('/', (req, res) => {
    res.send('Hello Fungie devs');
});

let user = {
    "name": "fungiedev",
    "currency": 100,
    "level": 25
}

let createdUsers = [
    {
        "id": 0,
        "name": "test0",
        "currency": 10,
        "level": 25
    },
    {
        "id": 1,
        "name": "test1",
        "currency": 10,
        "level": 25
    }
]


app.get('/user/info', (req, res) => {
    res.send(user);
});


app.post('/user/create', (req, res) => {

    console.log(req.body);

    let newUser = {
        "id": req.body.id,
        "name": req.body.name,
        "currency": req.body.currency,
        "level": req.body.level
    };
    
    createdUsers.push(newUser);
    console.log(createdUsers);
    res.send(createdUsers);

});

app.listen(3000, () => console.log('Started'));