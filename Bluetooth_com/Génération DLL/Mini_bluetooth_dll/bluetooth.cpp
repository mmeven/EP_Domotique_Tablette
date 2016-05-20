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
#include "bluetooth.h"

using namespace std;

extern "C" Q_DECL_EXPORT int getProfile(char* p){
    int profile = 9;
    QSerialPort *port = new QSerialPort(p);
    port->open(QIODevice::ReadWrite);
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
    port->close();
    return profile;
}
extern "C" Q_DECL_EXPORT int getSpeed(char* p){
    int speed = 15;
    int s1 = 0;
    int s2 = 0;
    QSerialPort *port = new QSerialPort(p);
    port->open(QIODevice::ReadWrite);
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
    port->close();
    return speed;
}

extern "C" Q_DECL_EXPORT int getMaximumSpeed(char* p){
    int speed = 15;
    QSerialPort *port = new QSerialPort(p);
    port->open(QIODevice::ReadWrite);
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
    port->close();
    return speed;
}
