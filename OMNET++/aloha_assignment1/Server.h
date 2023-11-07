
#ifndef __ALOHA_SERVER_H_
#define __ALOHA_SERVER_H_

#include <omnetpp.h>

using namespace omnetpp;

namespace aloha {

/**
 * Aloha server; see NED file for more info.
 */
class Server: public cSimpleModule {
private:
    // state variables, event pointers
    bool channelBusy;
    cMessage *endRxEvent = nullptr;

    intval_t currentCollisionNumFrames;
    intval_t receiveCounter;
    simtime_t lastArrival;
    simtime_t recvStartTime;
    enum {
        IDLE = 0, TRANSMISSION = 1, COLLISION = 2
    };
    simsignal_t channelStateSignal;

    int collision = 0;
    int packages = 0;

    // STATISTICS:
    simsignal_t receiveBeginSignal;
    simsignal_t receiveSignal;
    simsignal_t collisionLengthSignal;
    simsignal_t collisionSignal;
    simsignal_t packageSignal;

    //HISTOGRAMS & VECTORS:
    cHistogram collisionHistogram;
    cOutVector collisionVector;
    cHistogram packageHistogram;
    cOutVector packageVector;
public:
    virtual ~Server();

protected:
    virtual void initialize() override;
    virtual void handleMessage(cMessage *msg) override;
    virtual void finish() override;
    virtual void refreshDisplay() const override;
};

}
;
//namespace

#endif

