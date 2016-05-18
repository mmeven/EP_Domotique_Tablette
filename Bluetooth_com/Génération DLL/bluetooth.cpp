#include <QCoreApplication>
#include <QtWidgets/QApplication>
#include <QtSerialPort/qserialport.h>
#include <QtSerialPort/qserialportinfo.h>
#include <QtSerialPort/QSerialPort>
#include <QtSerialPort/QSerialPortInfo>
#include <QDebug>
#include <iostream>
#include <regex>
#include <boost/lexical_cast.hpp>
#include <unistd.h>
#include "bluetooth.h"

using namespace std;

extern "C" Q_DECL_EXPORT void openPort(char* port){
    QSerialPort *port = new QSerialPort("COM"+port);
    port->open(QIODevice::ReadWrite);
}

extern "C" Q_DECL_EXPORT void closePort(char* port){
    QSerialPort *port = new QSerialPort("COM"+port);
    port->close();
}

extern "C" Q_DECL_EXPORT int getVirtualJoystickPositionX(int port){
    int x = 15;
    try {
        if(port->isOpen() && port->isWritable()){
            const char output[10] = "AT+WGVP\r\n";
            port->write(output);
            port->flush();
            QByteArray input = port->readAll();
            while (port->waitForReadyRead(2000)) {
               input.append(port->readAll());
            }
            std::regex pattern { "(\\d+),\\d+" };
            std::string target { input.toStdString() };
            std::smatch match;
            std::regex_search(target, match, pattern);
            x = boost::lexical_cast<int>(match[1]);
        }
    } catch(std::exception const& e){
        x = -1;
    }
    return x;
}

extern "C" Q_DECL_EXPORT int getVirtualJoystickPositionY(QSerialPort *port){
    int y = 15;
    try {
        if(port->isOpen() && port->isWritable()){
            const char output[10] = "AT+WGVP\r\n";
            port->write(output);
            port->flush();
            QByteArray input = port->readAll();
            while (port->waitForReadyRead(2000)) {
               input.append(port->readAll());
            }
            std::regex pattern { "\\d+,(\\d+)" };
            std::string target { input.toStdString() };
            std::smatch match;
            std::regex_search(target, match, pattern);
            y = boost::lexical_cast<int>(match[1]);
        }
    }catch(std::exception const& e){
        y=-1;
    }
    return y;

}

extern "C" Q_DECL_EXPORT int getMaximumSpeed(QSerialPort *port){
    int speed = 15;
    try {
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
        }
    }catch(std::exception const& e){
        speed = -1;
    }

    return speed;
}

extern "C" Q_DECL_EXPORT int getSpeed(QSerialPort *port){
    int speed = 15;
    int s1 = 0;
    int s2 = 0;
    try {
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
            speed = (s2+s1)/256;
        }
    }catch(std::exception const& e){
        speed = -1;
    }

    return speed;
}

extern "C" Q_DECL_EXPORT int getProfile(QSerialPort *port){
    int profile = 9;
    try {
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
        }
    }catch(std::exception const& e){
        profile = -1;
    }

    return profile;
}

extern "C" Q_DECL_EXPORT int getBatteryLvl(QSerialPort *port){
    int battery = 10;
    try{
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
            battery = boost::lexical_cast<int>(match[1]);
        }
    }catch(std::exception const& e){
        battery = -1;
    }

    return battery;
}

extern "C" Q_DECL_EXPORT int getJoystickPositionX(QSerialPort *port){
    int x = 15;
    try {
        if(port->isOpen() && port->isWritable()){
            const char output[10] = "AT+WGVP\r\n";
            port->write(output);
            port->flush();
            QByteArray input = port->readAll();
            while (port->waitForReadyRead(2000)) {
               input.append(port->readAll());
            }
            std::regex pattern { "(\\d+),\\d+" };
            std::string target { input.toStdString() };
            std::smatch match;
            std::regex_search(target, match, pattern);
            x = boost::lexical_cast<int>(match[1]);
        }
    }catch(std::exception const& e){
        x = -1;
    }

    return x;
}

extern "C" Q_DECL_EXPORT int getJoystickPositionY(QSerialPort *port){
    int y = 15;
    try {
        if(port->isOpen() && port->isWritable()){
            const char output[10] = "AT+WGVP\r\n";
            port->write(output);
            port->flush();
            QByteArray input = port->readAll();
            while (port->waitForReadyRead(2000)) {
               input.append(port->readAll());
            }
            std::regex pattern { "\\d+,(\\d+)" };
            std::string target { input.toStdString() };
            std::smatch match;
            std::regex_search(target, match, pattern);
            y = boost::lexical_cast<int>(match[1]);
        }
    }catch(std::exception const& e){
        y = -1;
    }

    return y;
}

int main(int argc, char *argv[])
{
    QCoreApplication a(argc, argv);
    //QSerialPort *port = new QSerialPort("COM11");
    //port->open(QIODevice::ReadWrite);
    /*if(port->isOpen() && port->isWritable()){
        int x = getJoystickPositionX(port);
        int y = getJoystickPositionY(port);
        cout << "x = " << x << endl;
        cout << "y = " << y << endl;
        cout << "Vitesse : " << getSpeed(port) << endl;
        cout << "Vitesse maximale : " << getMaximumSpeed(port) << endl;
        cout << "Profil : " << getProfile(port) << endl;
        cout << "Batterie : " << getBatteryLvl(port) << endl;
    }*/
    getSpeed(11);
    port->close();
    a.quit();

}
