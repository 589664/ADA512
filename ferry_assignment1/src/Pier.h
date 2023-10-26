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

#ifndef PIER_H_
#define PIER_H_

#include <omnetpp.h>
using namespace omnetpp;

namespace ferry_assignment1 {

class Pier: public cSimpleModule {

private:
    int busQueueSize;   // Queue size for buses on the pier
    int carQueueSize;   // Queue size for cars on the pier
    int ferryEmptySpots;  // Number of empty spots on the ferry

    cOutVector queueSizeVector;
    cOutVector ferryEmptySpotsVector;

public:
    Pier();
    virtual ~Pier();

protected:
    virtual void initialize();
    virtual void handleMessage(cMessage *msg);
    virtual void finish();
};
}
;

#endif /* PIER_H_ */
