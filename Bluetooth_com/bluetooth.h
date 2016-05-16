#pragma once


extern "C" Q_DECL_EXPORT int getVirtualJoystickPositionX(QSerialPort *port);
extern "C" Q_DECL_EXPORT int getVirtualJoystickPositionY(QSerialPort *port);
extern "C" Q_DECL_EXPORT int getMaximumSpeed(QSerialPort *port);
extern "C" Q_DECL_EXPORT int getSpeed(QSerialPort *port);
extern "C" Q_DECL_EXPORT int getProfile(QSerialPort *port);
extern "C" Q_DECL_EXPORT int getBatteryLvl(QSerialPort *port);
extern "C" Q_DECL_EXPORT int getJoystickPositionX(QSerialPort *port);
extern "C" Q_DECL_EXPORT int getJoystickPositionY(QSerialPort *port);
