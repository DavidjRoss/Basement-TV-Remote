#include <IRremote.h>
#include "LivingRoom_TV_Controller.h"

IRsend irsend;
void setup() {
  Serial.begin(9600);
}

void loop() {
  if(Serial.available() > 0){
   byte received = Serial.read();

   if(received == RECEIVED_VOL_UP){
     irsend.sendPanasonic(PanasonicAddress,VOLUME_UP);
   }
   else if(received ==RECEIVED_VOL_DOWN){
     irsend.sendPanasonic(PanasonicAddress,VOLUME_DOWN);
   }
   else if(received == RECEIVED_POWER){
    irsend.sendPanasonic(PanasonicAddress,POWER);
   }
   else if(received == RECEIVED_INPUT){
     irsend.sendPanasonic(PanasonicAddress,CHANGE_INPUT);
   }
  }

}
