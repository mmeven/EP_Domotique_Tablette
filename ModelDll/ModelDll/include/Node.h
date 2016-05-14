#ifndef NODE_H_INCLUDED
#define NODE_H_INCLUDED
#include <wchar.h>

namespace EP {

	///<summary> Repr�sente un noeud de l'arbre (une pi�ce ou un �quipement)</br>
	///Cette classe est sp�cialis�e par les classes Equipment et Room, et n'est donc pas utilis�e
	///telle qu'elle.</summary>
	class __declspec(dllexport) Node {
	public:
		/// <summary>Constructeur, initialise les attributs m_name et m_ico</summary>
		Node(char* name, char* ico);

		///<summary>Destructeur, ne fait rien de particulier</summary>
		virtual ~Node(void);

		///<returns>Le nom du noeud.</returns>
		char* getName();

		///<summary>Change le nom de ce noeud pour celui pass� en param�tre.</summary>
		///<param name="name">Le nouveau nom du noeud.</param>
		void setName(char* name);

		///<returns>Le nom du fichier correspondant � l'ic�ne de ce noeud.</returns>
		char* getIco();

		///<summary>Change l'ic�ne du noeud.</summary>
		///<param name="name">La nouvelle ic�ne du noeud.</param>
		void setIco(char* ico);

	protected:
		///<summary>Le nom du noeud.</summary>
		char m_name[100];

		///<summary>L'ic�ne du noeud qui sera affich�e dans l'application.</summary>
		char m_ico[100];

		///<summary>Le noeud parent de celui-ci. Sera nul pour les pi�ces (Room),
		///et correspondra � la pi�ce contenante dans le cas d'un �quipement (Equipment).</summary>
		EP::Node* m_parent;
	};

	///<summary>Constructeur statique utilis� pour permettre l'utilisation des objets Node en
	///passant par la DLL. Les param�tres sont les m�mes que ceux du constructeur.</summary>
	///<returns>Un pointeur vers l'objet cr��.</returns>
	extern "C" __declspec(dllexport) Node* Node_New(char* name, char* ico);

	///<summary>Destructeur statique, pour permettre la destruction des objets Node en passant par la DLL.</summary>
	///<param name="node">Un pointeur vers l'objet � d�truire.</returns>
	extern "C" __declspec(dllexport) void Node_Delete(Node* node);
}
#endif