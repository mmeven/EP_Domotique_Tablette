#include "bluetooth.h"

using namespace std;

extern "C" Q_DECL_EXPORT QSerialPort* openPort(char* p){
    QSerialPort *port = new QSerialPort(p);
    port->open(QIODevice::ReadWrite);
    return port;
}

extern "C" Q_DECL_EXPORT void closePort(QSerialPort* port){
    port->close();
}

extern "C" Q_DECL_EXPORT int getVirtualJoystickPositionX(QSerialPort* port){
    int x = 15;
    if(!port->isOpen()) port->open(QIODevice::ReadWrite);

    try{
        if(port->isOpen() && port->isWritable()){
            // Envoi de la requete
            const char output[10] = "AT+WGVP\r\n";
            port->write(output);
            port->flush();

            // Lecture reponse
            QByteArray input = port->readAll();
            while (port->waitForReadyRead(2000)) {
                input.append(port->readAll());
            }
            cout << "Commande : " << input.data() << endl;
            // On parse et on recupere l'info
            std::regex pattern { "(-?\\d+),-?\\d+" };
            std::string target { input.toStdString() };
            std::smatch match;
            std::regex_search(target, match, pattern);
            x = boost::lexical_cast<int>(match[1]);

        }
    }
    catch(std::exception const& e){
        cerr << e.what();
    }
    return x;
}

extern "C" Q_DECL_EXPORT int getVirtualJoystickPositionY(QSerialPort *port){
    int y = 15;
    if(!port->isOpen()) port->open(QIODevice::ReadWrite);

    try{
        if(port->isOpen() && port->isWritable()){
            const char output[10] = "AT+WGVP\r\n";
            port->write(output);
            port->flush();

            QByteArray input = port->readAll();
            while (port->waitForReadyRead(2000)) {
                input.append(port->readAll());
            }
            cout << "Commande : " << input.data() << endl;
            std::regex pattern { "-?\\d+,(-?\\d+)" };
            std::string target { input.toStdString() };
            std::smatch match;
            std::regex_search(target, match, pattern);
            y = boost::lexical_cast<int>(match[1]);

        }
    }
    catch(std::exception const& e){
        cerr << e.what();
    }
    return y;
}

extern "C" Q_DECL_EXPORT int getMaximumSpeed(QSerialPort *port){
    int speed = 15;
    if(!port->isOpen()) port->open(QIODevice::ReadWrite);
    try{
        if(port->isOpen() && port->isWritable()){
            const char output[10] = "AT+WGMS\r\n";
            port->write(output);
            port->flush();

            QByteArray input = port->readAll();
            while (port->waitForReadyRead(2000)) {
                input.append(port->readAll());
            }
            cout << "Commande : " << input.data() << endl;
            std::regex pattern { ":(\\d+)" };
            std::string target { input.toStdString() };
            std::smatch match;
            std::regex_search(target, match, pattern);
            speed = boost::lexical_cast<int>(match[1]);

        }
    }
    catch(std::exception const& e){
        cerr << e.what();
    }
    return speed;
}

extern "C" Q_DECL_EXPORT float getSpeed(QSerialPort *port){
    float speed = 15;
    float s1 = 0;
    float s2 = 0;
    if(!port->isOpen()) port->open(QIODevice::ReadWrite);
    try{
        if(port->isOpen() && port->isWritable()){
            const char output[10] = "AT+WGSP\r\n";
            port->write(output);
            port->flush();

            QByteArray input = port->readAll();
            while (port->waitForReadyRead(2000)) {
                input.append(port->readAll());
            }
            cout << "Commande : " << input.data() << endl;
            std::regex pattern { ":(\\d+),(\\d+)" };
            std::string target { input.toStdString() };
            std::smatch match;
            std::regex_search(target, match, pattern);
            s1 = boost::lexical_cast<int>(match[1]);
            s2 = boost::lexical_cast<int>(match[2]);
            speed = s2 + s1/256;
            speed = speed*3.6;

        }
    }
    catch(std::exception const& e){
        cerr << e.what();
    }
    return speed;
}

extern "C" Q_DECL_EXPORT int getProfile(QSerialPort *port){
    int profile = 9;
    if(!port->isOpen()) port->open(QIODevice::ReadWrite);
    try{
        if(port->isOpen() && port->isWritable()){
            const char output[10] = "AT+WGMD\r\n";
            port->write(output);
            port->flush();

            QByteArray input = port->readAll();
            while (port->waitForReadyRead(2000)) {
                input.append(port->readAll());
            }
            cout << "Commande : " << input.data() << endl;
            std::regex pattern { ":(\\d+)" };
            std::string target { input.toStdString() };
            std::smatch match;
            std::regex_search(target, match, pattern);
            profile = boost::lexical_cast<int>(match[1]);

        }
    }
    catch(std::exception const& e){
        cerr << e.what();
    }
    return profile;
}

extern "C" Q_DECL_EXPORT int getBatteryLvl(QSerialPort *port){
    int battery = 10;
    if(!port->isOpen()) port->open(QIODevice::ReadWrite);
    try{
        if(port->isOpen() && port->isWritable()){
            const char output[10] = "AT+WGMD\r\n";
            port->write(output);
            port->flush();

            QByteArray input = port->readAll();
            while (port->waitForReadyRead(2000)) {
                input.append(port->readAll());
            }
            cout << "Commande : " << input.data() << endl;
            std::regex pattern { ":(\\d+)" };
            std::string target { input.toStdString() };
            std::smatch match;
            std::regex_search(target, match, pattern);
            battery = boost::lexical_cast<int>(match[1]);

        }
    }
    catch(std::exception const& e){
        cerr << e.what();
    }
    return battery;
}

extern "C" Q_DECL_EXPORT int getJoystickPositionX(QSerialPort *port){
    int x = 15;
    if(!port->isOpen()) port->open(QIODevice::ReadWrite);
    try{
        if(port->isOpen() && port->isWritable()){
            const char output[10] = "AT+WGVP\r\n";
            port->write(output);
            port->flush();

            QByteArray input = port->readAll();
            while (port->waitForReadyRead(2000)) {
                input.append(port->readAll());
            }
            cout << "Commande : " << input.data() << endl;
            std::regex pattern { "(-?\\d+),-?\\d+" };
            std::string target { input.toStdString() };
            std::smatch match;
            std::regex_search(target, match, pattern);
            x = boost::lexical_cast<int>(match[1]);

        }
    }
    catch(std::exception const& e){
        cerr << e.what();
    }
    return x;
}

extern "C" Q_DECL_EXPORT int getJoystickPositionY(QSerialPort *port){
    int y = 15;
    if(!port->isOpen()) port->open(QIODevice::ReadWrite);
    try{
        if(port->isOpen() && port->isWritable()){
            const char output[10] = "AT+WGVP\r\n";
            port->write(output);
            port->flush();
            QByteArray input = port->readAll();
            while (port->waitForReadyRead(2000)) {
                input.append(port->readAll());
            }
            cout << "Commande : " << input.data() << endl;
            std::regex pattern { "-?\\d+,(-?\\d+)" };
            std::string target { input.toStdString() };
            std::smatch match;
            std::regex_search(target, match, pattern);
            y = boost::lexical_cast<int>(match[1]);
        }
    }
    catch(std::exception const& e){
        cerr << e.what();
    }
    return y;
}

int main(int argc, char *argv[])
{
    QSerialPort* p = openPort("COM4");
    cout << getSpeed(p) << endl;
    closePort(p);
}
