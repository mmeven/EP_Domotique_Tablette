QT += serialport
QT -= gui

INCLUDEPATH += D:/Libs/boost_1_61_0/
LIBS += "-LD:/Libs/boost_1_61_0/"

CONFIG += dll

TARGET = Bluetooth_com
CONFIG += console
CONFIG -= app_bundle

TEMPLATE = lib

SOURCES += bluetooth.cpp

HEADERS += \
    bluetooth.h
