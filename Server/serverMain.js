const { MongoClient, ObjectID } = require("mongodb");
const Express = require("express");
const BodyParser = require('body-parser');
 
const server = Express();

server.use(BodyParser.json());
server.use(BodyParser.urlencoded({ extended: true }));

const client = new MongoClient("mongodb+srv://fornite:yPp2JIAhuwEYJmTh@cluster0.5h6km.mongodb.net/FORNITE?retryWrites=true&w=majority");
var collection;



server.listen("3000", async () => {
    try {
        await client.connect();
        collection = client.db("FORNITE").collection("userdata");
        console.log("Listening at :3000...");
       
        
    } catch (e) {
        console.error(e);
    }
});


server.post("/plummies", async (request, response, next) => {
    try {
        let result = await collection.insertOne(request.body);
       // console.log("Listening from server post");
       // console.log(request.body);
       // console.log(result);
        response.send(result);
    } catch (e) {
        response.status(500).send({ message: e.message });
    }
});
/*
server.get("/plummies", async (request, response, next) => {
    try {
      let result = await collection.find({}).toArray();
      response.send(result);
    } catch (e) {
        response.status(500).send({ message: e.message });
    }
});
*/
server.get("/plummies/:plummie_tag", async (request, response, next) => {

    try {
        //console.log("plummie_tag:  "+request.params.plummie_tag);
        let result = await collection.findOne({ "plummie_tag": request.params.plummie_tag });
        response.send(result);
        console.log("fetching data done!");
    } catch (e) {
        response.status(500).send({ message: e.message });
    }
});

server.put("/plummies/:plummie_tag", async (request, response, next) => {
    //console.log("Listening from Put");
    try {
        //console.log("plummie tag:  "+ request.params.plummie_tag);
        //console.log("resquest body: "+request.body);
        let result = await collection.updateOne(
            { "plummie_tag": request.params.plummie_tag },
            { "$set": request.body }
        );

        //console.log("result: "+result);
        response.send(result);
        console.log("update done");
    } catch (e) {
        response.status(500).send({ message: e.message });
    }
});

server.delete("/plummies/:plummie_tag", async (request, response, next) => {
    try {
        let result = await collection.deleteOne({ "plummie_tag": request.params.plummie_tag });
        response.send(result);
    } catch (e) {
        response.status(500).send({ message: e.message });
    }
});


