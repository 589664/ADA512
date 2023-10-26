//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with this program.  If not, see http://www.gnu.org/licenses/.
// 

#include "Vehicle.h"

namespace ferry_assignment1 {

Define_Module(Vehicle);

Vehicle::Vehicle() {
    carTimer = nullptr;
    busTimer = nullptr;
}

Vehicle::~Vehicle() {
    cancelAndDelete(carTimer);
    cancelAndDelete(busTimer);
}

void Vehicle::initialize() {
    carTimer = new cMessage("carTimer");
    busTimer = new cMessage("busTimer");

    scheduleAt(exponential(60), carTimer);
    scheduleAt(exponential(3600), busTimer);
}

void Vehicle::handleMessage(cMessage *msg) {
    if (msg == carTimer) {
        // Car event logic
        cMessage *carEvent = new cMessage("carEvent");
        send(carEvent, "outCar"); // Send the car event message to the "outCar" gate

        // Reschedule the carTimer for the next event in 60 seconds exponentially
        scheduleAt(simTime() + exponential(60), carTimer);
    } else if (msg == busTimer) {
        // Bus event logic
        cMessage *busEvent = new cMessage("busEvent");
        send(busEvent, "outBus"); // Send the bus event message to the "outBus" gate

        // Reschedule the busTimer for the next event in 3600 seconds (1 hour) exponentially
        scheduleAt(simTime() + exponential(3600), busTimer);
    } else {
        EV_ERROR << "Unexpected message received: " << msg->getName() << endl;
    }
}

}
;

