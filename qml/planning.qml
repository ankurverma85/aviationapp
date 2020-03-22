import QtQuick 2.12

Item {
    implicitWidth: stackView.width
    implicitHeight: stackView.height
    Rectangle {
        color: "white"
        anchors.fill: parent
    }
	Text {
		text: "Planning pane"
		anchors.centerIn: parent
	}
}