#ifndef NODE_H_INCLUDED
#define NODE_H_INCLUDED
#include <wchar.h>

namespace EP {

	///<summary> Représente un noeud de l'arbre (une pièce ou un équipement)</br>
	///Cette classe est spécialisée par les classes Equipment et Room, et n'est donc pas utilisée
	///telle qu'elle.</summary>
	class __declspec(dllexport) Node {
	public:
		/// <summary>Constructeur, initialise les attributs m_name et m_ico</summary>
		Node(char* name, char* ico);

		///<summary>Destructeur, ne fait rien de particulier</summary>
		virtual ~Node(void);

		///<returns>Le nom du noeud.</returns>
		char* getName();

		///<summary>Change le nom de ce noeud pour celui passé en paramètre.</summary>
		///<param name="name">Le nouveau nom du noeud.</param>
		void setName(char* name);

		///<returns>Le nom du fichier correspondant à l'icône de ce noeud.</returns>
		char* getIco();

		///<summary>Change l'icône du noeud.</summary>
		///<param name="name">La nouvelle icône du noeud.</param>
		void setIco(char* ico);

	protected:
		///<summary>Le nom du noeud.</summary>
		char m_name[100];

		///<summary>L'icône du noeud qui sera affichée dans l'application.</summary>
		char m_ico[100];

		///<summary>Le noeud parent de celui-ci. Sera nul pour les pièces (Room),
		///et correspondra à la pièce contenante dans le cas d'un équipement (Equipment).</summary>
		EP::Node* m_parent;
	};

	///<summary>Constructeur statique utilisé pour permettre l'utilisation des objets Node en
	///passant par la DLL. Les paramètres sont les mêmes que ceux du constructeur.</summary>
	///<returns>Un pointeur vers l'objet créé.</returns>
	extern "C" __declspec(dllexport) Node* Node_New(char* name, char* ico);

	///<summary>Destructeur statique, pour permettre la destruction des objets Node en passant par la DLL.</summary>
	///<param name="node">Un pointeur vers l'objet à détruire.</returns>
	extern "C" __declspec(dllexport) void Node_Delete(Node* node);
}
#endif