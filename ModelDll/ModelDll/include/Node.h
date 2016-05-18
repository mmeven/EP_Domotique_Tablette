#ifndef NODE_H_INCLUDED
#define NODE_H_INCLUDED

#ifndef DllExport
#define DllExport  __declspec(dllexport)
#endif

namespace EP {

	///\class Node
	///\brief Représente un noeud de l'arbre (une pièce ou un équipement).
	///Cette classe est spécialisée par les classes Equipment et Room, et n'est donc pas utilisée
	///telle qu'elle.
	class DllExport Node {
	public:
		/// \brief Constructeur, initialise les attributs m_name et m_ico
		Node(char* name, char* ico);

		///\brief Destructeur, ne fait rien de particulier
		virtual ~Node(void);

		///\return Le nom du noeud.
		char* getName();

		///\brief Change le nom de ce noeud pour celui passé en paramètre.
		///\param name Le nouveau nom du noeud.
		void setName(char* name);

		///\return Le nom du fichier correspondant à l'icône de ce noeud.
		char* getIco();

		///\brief Change l'icône du noeud.
		///\param ico La nouvelle icône du noeud.
		void setIco(char* ico);

	protected:
		///\brief Le nom du noeud.
		char m_name[100];

		///\brief L'icône du noeud qui sera affichée dans l'application.
		char m_ico[100];

		///\brief Le noeud parent de celui-ci. Sera nul pour les pièces (Room),
		///et correspondra à la pièce contenante dans le cas d'un équipement (Equipment).
		EP::Node* m_parent;
	};

	///\brief Constructeur statique utilisé pour permettre l'utilisation des objets Node en
	///passant par la DLL. Les paramètres sont les mêmes que ceux du constructeur.
	///\return Un pointeur vers l'objet créé.
	extern "C" DllExport Node* Node_New(char* name, char* ico);

	///\brief Destructeur statique, pour permettre la destruction des objets Node en passant par la DLL.
	///\param node Un pointeur vers l'objet à détruire.
	extern "C" DllExport void Node_Delete(Node* node);
}
#endif