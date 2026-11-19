using System;
using System.Net;               
using System.Net.Sockets;       
using System.Text;              



// 1) Opret en TcpListener der lytter på port 5000.
//    IPAddress.Any betyder "lyt på alle netværkskort på denne maskine".
TcpListener listener = new TcpListener(IPAddress.Any, 5000);

// 2) Start med at lytte.
listener.Start();
Console.WriteLine("Server started...");
Console.WriteLine("Waiting for client...");

// 3) Vent (blokerer her) indtil en client forbinder, og accepter forbindelsen.
//    AcceptTcpClient() giver os et TcpClient-objekt, der repræsenterer clienten.
TcpClient client = listener.AcceptTcpClient();
Console.WriteLine();
Console.WriteLine("Client connected!");

// 4) Hent forbindelsens NetworkStream. Det er gennem denne vi sender/modtager data.
NetworkStream stream = client.GetStream();

// 5) Modtag en besked fra clienten.
//    En buffer er bare et "opsamlingssted" (array af bytes) til de data vi læser.
byte[] buffer = new byte[1024];

//    Read() lægger de modtagne bytes i buffer og returnerer hvor mange bytes der kom.
int bytesRead = stream.Read(buffer, 0, buffer.Length);

//    Konverter de modtagne bytes tilbage til tekst (kun de bytes vi faktisk fik).
string received = Encoding.UTF8.GetString(buffer, 0, bytesRead);
Console.WriteLine("Received: " + received);

// 6) Send "PONG" tilbage til clienten.
string response = "PONG";
Console.WriteLine("Sending: " + response);

//    Teksten skal konverteres til bytes før den kan sendes over streamen.
byte[] data = Encoding.UTF8.GetBytes(response);
stream.Write(data, 0, data.Length);

// 7) Ryd op: luk stream og forbindelse.
stream.Close();
client.Close();
listener.Stop();

// Hold konsolvinduet åbent så vi kan nå at læse outputtet.
Console.WriteLine();
Console.WriteLine("Tryk på en tast for at afslutte...");
Console.ReadKey();
