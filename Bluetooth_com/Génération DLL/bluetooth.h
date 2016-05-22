#pragma once
#include <QtSerialPort/qserialport.h>
#include <QtSerialPort/QSerialPort>
#include <iostream>
#include <regex>
#include <boost/lexical_cast.hpp>

extern "C" Q_DECL_EXPORT QSerialPort* openPort(char* p);
extern "C" Q_DECL_EXPORT void closePort(QSerialPort* port);
extern "C" Q_DECL_EXPORT int getVirtualJoystickPositionX(QSerialPort *port);
extern "C" Q_DECL_EXPORT int getVirtualJoystickPositionY(QSerialPort *port);
extern "C" Q_DECL_EXPORT int getMaximumSpeed(QSerialPort *port);
extern "C" Q_DECL_EXPORT float getSpeed(QSerialPort *port);
extern "C" Q_DECL_EXPORT int getProfile(QSerialPort *port);
extern "C" Q_DECL_EXPORT int getBatteryLvl(QSerialPort *port);
extern "C" Q_DECL_EXPORT int getJoystickPositionX(QSerialPort *port);
extern "C" Q_DECL_EXPORT int getJoystickPositionY(QSerialPort *port);
