#include <QCoreApplication>
#include <QtWidgets/QApplication>
#include <QtSerialPort/qserialport.h>
#include <QtSerialPort/qserialportinfo.h>
#include <QtSerialPort/QSerialPort>
#include <QtSerialPort/QSerialPortInfo>
#include <QDebug>
#include <QTimer>
#include <iostream>
#include <regex>
#include <boost/lexical_cast.hpp>
#include "bluetooth.h"

using namespace std;

QSerialPort *port = new QSerialPort("COM9");
void openPort(char *p){
    if(port->open(QIODevice::ReadWrite)){
        cout << "Ouverture du port" << endl;
    }
}

void closePort(char* p){
    QSerialPort *port = new QSerialPort(p);
    port->close();
}

int getVirtualJoystickPositionX(char* p){
    int x = 15;
    if(!port->isOpen()){
        cout << "Le port est ferme c'est con !" << endl;
    }
    if(port->isOpen() && port->isWritable()){
        cout << "Port ouvert" << endl;
        const char output[10] = "AT+WGVP\r\n";
        if(port->write(output)){
            cout << "Requete ecrite" << endl;
        }
        if(port->flush()){
            cout << "Flush" << endl;
        }
        QByteArray input = port->readAll();
        while (port->waitForReadyRead(2000)) {
           input.append(port->readAll());
           cout << "Lu" << endl;
        }
        std::regex pattern { "(\\d+),\\d+" };
        std::string target { input.toStdString() };
        std::smatch match;
        std::regex_search(target, match, pattern);
        x = boost::lexical_cast<int>(match[1]);
    }
    return x;
}

int getVirtualJoystickPositionY(char* p){
    int y = 15;
    QSerialPort *port = new QSerialPort(p);
    if(!port->isOpen()){
        cout << "Le port est ferme c'est con !" << endl;
    }
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

    return y;
}

int getMaximumSpeed(char* p){
    int speed = 15;
    QSerialPort *port = new QSerialPort(p);
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
    return speed;
}

int getSpeed(char* p){
    int speed = 15;
    int s1 = 0;
    int s2 = 0;
    QSerialPort *port = new QSerialPort(p);
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
    return speed;
}

int getProfile(char* p){
    int profile = 9;
    QSerialPort *port = new QSerialPort(p);
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
    return profile;
}

int getBatteryLvl(char* p){
    int battery = 10;
    QSerialPort *port = new QSerialPort(p);
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
    return battery;
}

int getJoystickPositionX(char* p){
    int x = 15;
    QSerialPort *port = new QSerialPort(p);
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
    return x;
}

int getJoystickPositionY(char* p){
    int y = 15;
    QSerialPort *port = new QSerialPort(p);
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
    return y;
}

int main(int argc, char *argv[])
{
    QCoreApplication a(argc, argv);
    openPort((char*)"COM9");
    //boost::this_thread::sleep( boost::posix_time::seconds(1) );
    cout << "Virtual Joystick x : " << getVirtualJoystickPositionX((char*)"COM9") << endl;
    cout << "Virtual Joystick y : " << getVirtualJoystickPositionY((char*)"COM9") << endl;
    cout << "Vitesse max : " << getMaximumSpeed((char*)"COM9") << endl;
    cout << "Vitesse : " << getSpeed((char*)"COM9") << endl;
    closePort((char*)"COM9");
    a.quit();

}
