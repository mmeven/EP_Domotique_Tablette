QT += core
QT += widgets
QT += serialport
QT -= gui

CONFIG += dll

TARGET = Bluetooth_com
CONFIG += console
CONFIG -= app_bundle

TEMPLATE = lib

SOURCES += bluetooth.cpp

HEADERS += \
    bluetooth.h
