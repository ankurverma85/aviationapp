import QtQuick 2.12

Item {
    implicitWidth: stackView.width
    implicitHeight: stackView.height
    Rectangle {
        color: "white"
        anchors.fill: parent
    }
	Text {
        id: wb_text
		text: "W&B pane"
		anchors.centerIn: parent
	}
}