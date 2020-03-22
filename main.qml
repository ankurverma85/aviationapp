import QtQuick 2.12
import OpenGLUnderQML 1.0
import QtQuick.Controls 2.12
import QtQuick.Window 2.12

ApplicationWindow {
    id: applicationWindow
    visibility: Window.AutomaticVisibility
    visible: true
    minimumHeight: 480
    minimumWidth: 320

    Item {
        anchors.fill: parent

        Squircle {
            SequentialAnimation on t {
                NumberAnimation { to: 1; duration: 2500; easing.type: Easing.InQuad }
                NumberAnimation { to: 0; duration: 2500; easing.type: Easing.OutQuad }
                loops: Animation.Infinite
                running: true
            }
        }
        Rectangle {
            color: Qt.rgba(1, 1, 1, 0.7)
            radius: 10
            border.width: 1
            border.color: "white"
            anchors.fill: label
            anchors.margins: -10
        }

        Text {
            id: label
            color: "black"
            wrapMode: Text.WordWrap
            text: "The background here is a squircle rendered with raw OpenGL using the 'beforeRender()' signal in QQuickWindow. This text label and its border is rendered using QML"
            anchors.right: parent.right
            anchors.left: parent.left
            anchors.bottom: parent.bottom
            anchors.margins: 20
        }
    }

    Drawer {
        id: navigationDrawer
        width: Math.min(applicationWindow.width, 200)
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
                    onTapped: console.log(nav + " tapped")
                }
            }
        }
    }
}