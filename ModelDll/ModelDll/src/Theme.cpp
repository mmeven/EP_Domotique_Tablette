#include "../include/Theme.h"

namespace EP {
	Color::Color(int r, int g, int b) : red(r), green(g), blue(b) {	}

	Color::~Color(){}

	int Color::getRed() {
		return red;
	}

	int Color::getGreen() {
		return green;
	}

	int Color::getBlue() {
		return blue;
	}

	extern "C" __declspec(dllexport) Color* Color_New(int r, int g, int b) {
		return new Color(r, g, b);
	}

	extern "C" __declspec(dllexport) void Color_Delete(Color* color) {
		delete color;
	}

	Theme::Theme(Color* grid, Color* bar, Color* buttons, Color* activeButton, Color* emptyButton, Color* bg, Color* header)
		: m_grid(grid), m_bar(bar), m_buttons(buttons), m_activeButton(activeButton), m_emptyButton(emptyButton), m_bg(bg), m_header(header) {}

	Theme::~Theme(){}

	Color* Theme::getColorGrid() {
		return m_grid;
	}
	Color* Theme::getColorBar() {
		return m_bar;
	}
	Color* Theme::getColorButtons() {
		return m_buttons;
	}
	Color* Theme::getColorActiveButton() {
		return m_activeButton;
	}
	Color* Theme::getColorEmptyButton() {
		return m_emptyButton;
	}
	Color* Theme::getColorBackground() {
		return m_bg;
	}
	Color* Theme::getColorHeader() {
		return m_header;
	}
}