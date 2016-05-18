QT += core
QT += widgets
QT += serialport
QT -= gui

CONFIG += c++11

TARGET = Bluetooth_tests
CONFIG += console
CONFIG -= app_bundle

TEMPLATE = app

SOURCES += bluetooth.cpp

HEADERS += bluetooth.h
