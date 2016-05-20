#include "bluetooth.h"

using namespace std;

extern "C" Q_DECL_EXPORT int getProfile(QSerialPort* port){
    int profile = 9;
    while(!port->isOpen()) port->open(QIODevice::ReadWrite);
    if(port->isOpen() && port->isWritable()){
        const char output[10] = "AT+WGMD\r\n";
        port->write(output);
        port->flush();
        QByteArray input = port->readAll();
        while (port->waitForReadyRead(2000)) {
           input.append(port->readAll());
        }
        std::regex pattern { ":(\\d+)" };
        std::string target { input.toStdString() };
        std::smatch match;
        std::regex_search(target, match, pattern);
        profile = boost::lexical_cast<int>(match[1]);
        char* toast = input.data();
        cout << toast << endl;
    }
    return profile;
}
extern "C" Q_DECL_EXPORT float getSpeed(QSerialPort* port){
    float speed = 15;
    float s1 = 0;
    float s2 = 0;
    while(!port->isOpen()) port->open(QIODevice::ReadWrite);
    if(port->isOpen() && port->isWritable()){
        const char output[10] = "AT+WGSP\r\n";
        port->write(output);
        port->flush();
        QByteArray input = port->readAll();
        while (port->waitForReadyRead(2000)) {
           input.append(port->readAll());
        }
        std::regex pattern { ":(\\d+),(\\d+)" };
        std::string target { input.toStdString() };
        std::smatch match;
        std::regex_search(target, match, pattern);
        s1 = boost::lexical_cast<int>(match[1]);
        s2 = boost::lexical_cast<int>(match[2]);
        speed = s2 + s1/256;
        char* toast = input.data();
        cout << "s1 = " << s1 << ", s2 = " << s2 << ", commande : " << toast << endl;
    }
    return speed;
}

extern "C" Q_DECL_EXPORT int getMaximumSpeed(QSerialPort* port){
    int speed = 15;
    while(!port->isOpen()) port->open(QIODevice::ReadWrite);
    if(port->isOpen() && port->isWritable()){
        const char output[10] = "AT+WGMS\r\n";
        port->write(output);
        port->flush();
        QByteArray input = port->readAll();
        while (port->waitForReadyRead(2000)) {
           input.append(port->readAll());
        }
        std::regex pattern { ":(\\d+)" };
        std::string target { input.toStdString() };
        std::smatch match;
        std::regex_search(target, match, pattern);
        speed = boost::lexical_cast<int>(match[1]);
        char* toast = input.data();
        cout << toast << endl;
    }
    return speed;
}

extern "C" Q_DECL_EXPORT QSerialPort* openPort(char* port){
    QSerialPort* ret = new QSerialPort(port);
    while(!ret->isOpen()) ret->open(QIODevice::ReadWrite);
    return ret;
}

extern "C" Q_DECL_EXPORT void closePort(QSerialPort* port){
    port->close();
}
