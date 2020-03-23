import QtQuick 2.12
import QtQuick.Controls 2.12
import QtQuick.Window 2.12

ApplicationWindow {
    id: applicationWindow
    visibility: Window.AutomaticVisibility
    visible: true
    minimumHeight: 480
    minimumWidth: 320

    StackView {
        id: stackView
        anchors.fill: parent
        initialItem: "qrc:/qml/map.qml"
    }

    Drawer {
        id: navigationDrawer
        width: Math.max(Math.min(applicationWindow.width, 200), 0.67 * applicationWindow.width)
        height: applicationWindow.height

        Label {
            text: "Navigation drawer"
            anchors.centerIn: parent
        }

        ListView {
            id: navigationListView
            anchors.fill: parent
            model: ListModel {
                ListElement { nav: "map" }
                ListElement { nav: "planning" }
                ListElement { nav: "wb" }
            }
            delegate: Rectangle {
                width: parent.width
                height: childrenRect.height
                Text {
                    text: nav
                    font.pointSize: 20
                }
                TapHandler {
                    onTapped: {
                        console.log(nav + " tapped")
                        stackView.replace("qrc:/qml/" + nav + ".qml")
                    }
                }
            }
        }
    }
}