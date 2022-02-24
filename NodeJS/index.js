const { createServer } = require("http");
const { Server } = require("socket.io");
const fs = require("fs");
const lights = require("./lights/SetLights");

const httpServer = createServer((req, res) => {
  fs.readFile(__dirname + '/ui/client.html', (err, data) =>{
    res.writeHead(200);
    res.end(data);
  })
});

const io = new Server(httpServer, {
  cors: {
    origin: "http://localhost:8080",
    credentials: true
  },
  allowEIO3: true,
  maxHttpBufferSize: 1e8
});

function log(message){
  console.log(`INFO | ${message}`);
}

io.on("connection", (socket) => {
  socket.emit("toNodeUI", "Hello Node UI");

  /**
  * Event sent from the UI for this server
  * data type : {
    *      destination: string -> the name of the event to emit with the message
    *      message: string/object (to be defined) -> the data to send with the event
    * }
    */
    socket.on("fromNodeUI", (data) => {
      log(`Received data from UI: ${JSON.stringify(data)}`);
      io.emit(data.destination, data.message);

      if(data.destination == "changeLights"){ // Temporaire le temps d'ajouter une interface sur l'écran
        log(`Change Lights : ${data.message}`);
        lights.setLights(data.message);
      }
    });

    socket.on("eventFromScreen", (data) => {
      log(`Message from screen : ${data}`);
    });

    socket.on("fromUnity", (data) => {
      log(`Message from unity : ${data}`);
      io.emit('eventToScreen',data);
    });

    socket.on("newBPM", (data) => {
      const start = Date.now() + 2000;
      const objBPM = JSON.stringify({ bpm: data, timestamp:start});
      log(`Change BPM : ${objBPM}`);
      io.emit('changeBPM', objBPM)
    });

    socket.on("changeLights", (data) => {
      log(`Change Lights : ${data}`);
      io.emit('newLights', data);
      //lights.setLights(data);
    });

    socket.on("voiceFromPhone",(data)=>{
      console.log(Buffer.from(data));
      fs.writeFileSync(__dirname + '/sounds/testVoix.wav', Buffer.from(data));
      io.emit('newVoice',data);
    })

    socket.on("sendRecordA", (data) => {
      // Uncomment the following lines to receive a file from Unity
      // const buffer = Buffer.from(data.record);
      // console.log(buffer);
      // io.emit('addRecord', {zone: 'A', instrument: data.instrument, record: buffer});
      
      //  const fileData = fs.readFileSync(__dirname + '/sounds/Ulysse31-Oscillian-Remix.wav');
       const fileData = fs.readFileSync(__dirname + '/sounds/recTest.wav');
      //  const fileData = fs.readFileSync(__dirname + '/sounds/Guitare.wav');
       //console.log(fileData);
       io.emit('addRecord', {zone: 'D', instrument: 'guitar', record: fileData});

    })

    socket.on("sendRecordB", (data) => {
      const buffer = Buffer.from(data.record);
      console.log(buffer);
      io.emit('addRecord', {zone: 'B', instrument: data.instrument, record: buffer});
    })

    socket.on("sendRecordC", (data) => {
      const buffer = Buffer.from(data.record);
      console.log(buffer);
      io.emit('addRecord', {zone: 'C', instrument: data.instrument, record: buffer});
    })

    socket.on("sendRecordD", (data) => {
      const buffer = Buffer.from(data.record);
      console.log(buffer);
      io.emit('addRecord', {zone: 'D', instrument: data.instrument, record: buffer});
    })

    console.log(`Client connected - ${socket.id}`);
  });

  httpServer.listen(3000);
  console.log('Listening on port 3000');
