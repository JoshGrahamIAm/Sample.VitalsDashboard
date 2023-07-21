var isConnected = false;
var connection = new signalR.HubConnectionBuilder()
  .withUrl("/heartratehub")
  .withAutomaticReconnect({
    nextRetryDelayInMilliseconds: retryContext => {
      if (retryContext.elapsedMilliseconds < 60000) {
        // If we've been reconnecting for less than 60 seconds so far,
        // wait between 0 and 10 seconds before the next reconnect attempt.
        return Math.random() * 10000;
      } else {
        // If we've been reconnecting for more than 60 seconds so far, stop reconnecting.
        return null;
      }
    }
  })
  .build();


function startConnection() {
  try {
    connection.start();
 
    console.log("connected");
  } catch (err) {
   
    console.log(err);
    setTimeout(() => start(), 5000);
  }
};

startConnection();

connection.on("ReceiveHeartRateUpdate",
    function (heartRateValue, rate, sat) {
        console.log(heartRateValue);
        $("#heartRateData .card-text").text("Heart Rate: " + rate + " bpm");
        $("#oxygenData .card-text").text("Heart Rate: " + sat + " %SpO2");
       });

        connection.onreconnecting(error => {
        });

connection.onclose(async () => {

  console.log("connection lost");
});

