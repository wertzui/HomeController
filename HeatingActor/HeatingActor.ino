/*
 Name:		HeatingActor.ino
 Created:	4/30/2016 6:12:34 PM
 Author:	spbwe
*/
#include <SPI.h>
#include <Ethernet.h>
#include <aREST.h>
#include <avr/wdt.h>

#define LIGHTWEIGHT 1

#define RELAY_ON 0
#define RELAY_OFF 1
#define Relay_1  2  // Arduino Digital I/O pin number
#define Relay_2  3
#define Relay_3  4
#define Relay_4  5
#define Relay_5  6
#define Relay_6  7
#define Relay_7  8
#define Relay_8  9

#define Relay_Wohnzimmer Relay_1
#define Relay_Schlafzimmer Relay_2
#define Relay_Kinderzimmer1 Relay_3
#define Relay_Kinderzimmer2 Relay_4
#define Relay_Bad Relay_5
#define Relay_WC Relay_6

byte mac[] = { 0x90, 0xA2, 0xDA, 0x0E, 0xFE, 0x40 };
EthernetServer server(80);
aREST rest = aREST();



// the setup function runs once when you press reset or power the board
void setup() {
	Serial.begin(115200);
	initRelays();

	initServer();
	initFunctions();
	Serial.println("Setup completed");
}

// the loop function runs over and over again until power down or reset
void loop() {
	// listen for incoming clients
	EthernetClient client = server.available();
	rest.handle(client);
	wdt_reset();
}

void initServer() {
	Serial.println("Starting Ethernet");
	Ethernet.begin(mac);
	Serial.println("Starting REST Server");
	server.begin();
	Serial.print("server is at ");
	Serial.println(Ethernet.localIP());
	wdt_enable(WDTO_4S);
}

void initFunctions() {
	rest.function("heizung", &heating);
}

void initRelays() {
	Serial.println("Resetting Relais");
	//-------( Initialize Pins so relays are inactive at reset)----
	digitalWrite(Relay_1, RELAY_OFF);
	digitalWrite(Relay_2, RELAY_OFF);
	digitalWrite(Relay_3, RELAY_OFF);
	digitalWrite(Relay_4, RELAY_OFF);
	digitalWrite(Relay_5, RELAY_OFF);
	digitalWrite(Relay_6, RELAY_OFF);
	digitalWrite(Relay_7, RELAY_OFF);
	digitalWrite(Relay_8, RELAY_OFF);


	//---( THEN set pins as outputs )----
	pinMode(Relay_1, OUTPUT);
	pinMode(Relay_2, OUTPUT);
	pinMode(Relay_3, OUTPUT);
	pinMode(Relay_4, OUTPUT);
	pinMode(Relay_5, OUTPUT);
	pinMode(Relay_6, OUTPUT);
	pinMode(Relay_7, OUTPUT);
	pinMode(Relay_8, OUTPUT);
	delay(4000); //Check that all relays are inactive at Reset
	Serial.println("Done resetting Relais");
}

int heating(String roomAndState) {
	int separatorIndex = roomAndState.indexOf("|");
	if (separatorIndex < 0)
		return -1;
	String room = roomAndState.substring(0, separatorIndex);
	String state = roomAndState.substring(separatorIndex + 1);
	Serial.print("Setting room ");
	Serial.print(room);
	Serial.print(" to ");
	Serial.println(state);
	return setRelay(room, state);
}

int setRelay(String room, String state) {
	return setRelay(getPin(room), state);
}

int setRelay(int pin, String state) {
	Serial.print("Setting relays on pin ");
	Serial.print(pin);
	Serial.print(" to ");
	Serial.println(state);
	if (pin < 2 || pin > 9)
		return -1;

	if (state.equalsIgnoreCase("on") || state.equalsIgnoreCase("1")) {
		digitalWrite(pin, RELAY_ON);
		return 1;
	}

	digitalWrite(pin, RELAY_OFF);
	return 0;
}

uint8_t getPin(String room) {
	if (room.equalsIgnoreCase("wohnzimmer"))
		return Relay_Wohnzimmer;
	if (room.equalsIgnoreCase("schlafzimmer"))
		return Relay_Schlafzimmer;
	if (room.equalsIgnoreCase("kinderzimmer1"))
		return Relay_Kinderzimmer1;
	if (room.equalsIgnoreCase("kinderzimmer2"))
		return Relay_Kinderzimmer2;
	if (room.equalsIgnoreCase("bad"))
		return Relay_Bad;
	if (room.equalsIgnoreCase("wc"))
		return Relay_WC;
}