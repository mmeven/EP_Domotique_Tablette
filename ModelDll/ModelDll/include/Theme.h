#ifndef THEME_H_INCLUDED
#define THEME_H_INCLUDED

namespace EP {
	
	class __declspec(dllexport) Color {
	public:
		Color(int r, int g, int b);
		~Color();

		int getRed();

		int getGreen();

		int getBlue();
	private:
		int red;
		int green;
		int blue;
	};

	extern "C" __declspec(dllexport) Color* Color_New(int r, int g, int b);

	extern "C" __declspec(dllexport) void Color_Delete(Color* color);

	class __declspec(dllexport) Theme {
	public:
		Theme(Color* grid, Color* bar, Color* buttons, Color* activeButton, Color* emptyButton, Color* bg, Color* header);
		~Theme();

		Color* getColorGrid();
		Color* getColorBar();
		Color* getColorButtons();
		Color* getColorActiveButton();
		Color* getColorEmptyButton();
		Color* getColorBackground();
		Color* getColorHeader();
	private:
		Color* m_grid;
		Color* m_bar;
		Color* m_buttons;
		Color* m_activeButton;
		Color* m_emptyButton;
		Color* m_bg;
		Color* m_header;
	};

	extern "C" __declspec(dllexport) Theme* Theme_New(Color* grid, Color* bar, Color* buttons, Color* activeButton, Color* emptyButton, Color* bg, Color* header);

	extern "C" __declspec(dllexport) void Theme_Delete(Theme* theme);
}

#endif