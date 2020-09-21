using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NPCHerbalista : MonoBehaviour
{
    [Header("Variaveis de Interação com o npc")]
    public bool Conversando;
    public GameObject[] Imagens; //Escolha(1), Poções(2), Vender(3), Item vendido com Sucesso(4)

    //Parte da tela 2 "poções"
    [Header("Variaveis da escolha poções")]
    public GameObject[] ChangeButtonForText;

    //Textos que serao trocas por strings 
    [Header("Textos a serem trocados por strings")]
    public Text[] TrocaStrings;

    //Atributos referencia
    [Header("Variaveis do personagem")]
    public Transform Boy;

    //Itens Necessarios para Comprar e vender
    [Header("Controle Menu de Loot")]
    public Transform ControleDeItens;


    //Parte da tela 3 "venda de itens"
    [Header("Variaveis de botoes das vendas")]
    public GameObject[] AparaceBotoesVenda;
    public int Itens;
    public int Grana;

    //Textos que serao trocas por strings 
    [Header("Textos a serem trocados")]
    public Text[] ChangingText;
    public float Escolhido;

    // Start is called before the first frame update
    void Start()
    {
        //Atributos do boy
        Boy = GameObject.Find("boy").transform;

        //Controle do Menu de Loot
        ControleDeItens = GameObject.Find("Controle de Items").transform;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            Vector3 direction = Boy.position - this.transform.position;
            direction.y = 0;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);


            if (Input.GetKeyDown(KeyCode.I) && Conversando == false)
            {
                AbreImagem1();
            }
        }
    }
    //Primeira Imagem de "escolhas"
    public void AbreImagem1()
    {
        Imagens[0].SetActive(true);
        Imagens[1].SetActive(false);
        Imagens[2].SetActive(false);
        Imagens[3].SetActive(false);
        Conversando = true;
        Boy.GetComponent<MovementBoy>().PersonagemImovelParaTudo = true;
        Boy.GetComponent<MovementBoy>().Correndo = false;
        Boy.GetComponent<MovementBoy>().Idle = true;

    }
    //Fecha a primeira Imagem --> para de falar com NPC
    public void CloseAll()
    {
        Imagens[0].SetActive(false);
        Conversando = false;
        Boy.GetComponent<MovementBoy>().PersonagemImovelParaTudo = false;
    }
    //***********************************************************************************************************************************

    //Segunda Imagem de "poções"
    public void AbreImagem2()
    {
        Imagens[0].SetActive(false);
        Imagens[1].SetActive(true);
        Imagens[2].SetActive(false);
        Imagens[3].SetActive(false);

        verificaPocoes();
    }
    //Segunda Imagem de "poções"
    public void verificaPocoes()
    {
        if (Boy.GetComponentInChildren<BoyHP>().TemPocao1 == true) {

            ChangeButtonForText[0].SetActive(false); 
            ChangeButtonForText[1].SetActive(true);  //Ativa o texto e desativa a opção de compra (botão)
            TrocaStrings[0].text = "- Você só pode ter uma dessa poção!";

        }
        else
        {
            if (Boy.GetComponentInChildren<LevelBoy>().Moedas >= 50 && ControleDeItens.GetComponent<MenuDeLoot>().TypeOfFlowers[0] >= 5)
            {
                ChangeButtonForText[0].SetActive(true); //Desativa o texto e ativa a opção de compra (botão)
                ChangeButtonForText[1].SetActive(false);
            }
            else
            {
                ChangeButtonForText[0].SetActive(false);
                ChangeButtonForText[1].SetActive(true);  //Ativa o texto e desativa a opção de compra (botão)
                TrocaStrings[0].text = "- Faltam itens...";
            }
        }
        if (Boy.GetComponentInChildren<BoyHP>().TemPocao2 == true)
        {

            ChangeButtonForText[2].SetActive(false);
            ChangeButtonForText[3].SetActive(true);  //Ativa o texto e desativa a opção de compra (botão)
            TrocaStrings[1].text = "- Você só pode ter uma dessa poção!";
        }
        else
        {
            if (Boy.GetComponentInChildren<LevelBoy>().Moedas >= 100 && ControleDeItens.GetComponent<MenuDeLoot>().TypeOfFlowers[1] >= 5)
            {
                ChangeButtonForText[2].SetActive(true); //Desativa o texto e ativa a opção de compra (botão)
                ChangeButtonForText[3].SetActive(false);
            }
            else
            {
                ChangeButtonForText[2].SetActive(false);
                ChangeButtonForText[3].SetActive(true);  //Ativa o texto e desativa a opção de compra (botão)
                TrocaStrings[1].text = "- Faltam itens...";
            }
        }
    }

    //comprar poção de vida
    public void ComprarVida1()
    {
        Boy.GetComponentInChildren<BoyHP>().TemPocao1 = true;
        Boy.GetComponentInChildren<LevelBoy>().Moedas = Boy.GetComponentInChildren<LevelBoy>().Moedas - 50;
        ControleDeItens.GetComponent<MenuDeLoot>().TypeOfFlowers[0] = ControleDeItens.GetComponent<MenuDeLoot>().TypeOfFlowers[0] - 5;
        verificaPocoes();
        Boy.GetComponentInChildren<BoyHP>().SaveInfoBoyHP();
        Boy.GetComponentInChildren<AtributosBoy>().SaveInfoBoyAtributos();
        Boy.GetComponentInChildren<WeaponManager>().SaveInfoBoyWEAPONS();
        Boy.GetComponentInChildren<LevelBoy>().SaveInfoBoyLEVEL();
        Boy.GetComponentInChildren<MissoesManager>().SaveInfoBoyMISSION();
        ControleDeItens.GetComponent<MenuDeLoot>().SaveInfoBoyLOOTS();

        ControleDeItens.GetComponent<MenuDeLoot>().AtualizarRecursos();
    }
    //comprar poção de vida2
    public void ComprarVida2()
    {
        Boy.GetComponentInChildren<BoyHP>().TemPocao2 = true;
        Boy.GetComponentInChildren<LevelBoy>().Moedas = Boy.GetComponentInChildren<LevelBoy>().Moedas - 100;
        ControleDeItens.GetComponent<MenuDeLoot>().TypeOfFlowers[1] = ControleDeItens.GetComponent<MenuDeLoot>().TypeOfFlowers[1] - 5;
        verificaPocoes();
        Boy.GetComponentInChildren<BoyHP>().SaveInfoBoyHP();
        Boy.GetComponentInChildren<AtributosBoy>().SaveInfoBoyAtributos();
        Boy.GetComponentInChildren<WeaponManager>().SaveInfoBoyWEAPONS();
        Boy.GetComponentInChildren<LevelBoy>().SaveInfoBoyLEVEL();
        Boy.GetComponentInChildren<MissoesManager>().SaveInfoBoyMISSION();
        ControleDeItens.GetComponent<MenuDeLoot>().SaveInfoBoyLOOTS();

        ControleDeItens.GetComponent<MenuDeLoot>().AtualizarRecursos();
    }
    //***********************************************************************************************************************************
    //Terceira Imagem de "itens vendidos"
    public void AbreImagem3()
    {
        Imagens[0].SetActive(false);
        Imagens[1].SetActive(false);
        Imagens[2].SetActive(true);
        Imagens[3].SetActive(false);
        Itens = 0;
        ChangingText[0].text = "" + Itens;
        Grana = 0;
        ChangingText[1].text = "" + Grana;
        //verificaVendas();
    }

    public void EscolheItemLoot(int val)
    {
        if (val == 0)
        {
            Escolhido = 0.0f;
            Itens = 0;
            ChangingText[0].text = "" + Itens;
            Grana = 0;
            ChangingText[1].text = "" + Grana;
            AparaceBotoesVenda[0].SetActive(true); //Botao de vender reaparece
        }
        if (val == 1)
        {
            Escolhido = 0.2f;
            Itens = 0;
            ChangingText[0].text = "" + Itens;
            Grana = 0;
            ChangingText[1].text = "" + Grana;
            if (Itens <= ControleDeItens.GetComponent<MenuDeLoot>().LootCobra[0])
            {
                AparaceBotoesVenda[0].SetActive(true); //Botao de vender reaparece
            }
        }
        if (val == 2)
        {
            Escolhido = 0.3f;
            Itens = 0;
            ChangingText[0].text = "" + Itens;
            Grana = 0;
            ChangingText[1].text = "" + Grana;
            if (Itens <= ControleDeItens.GetComponent<MenuDeLoot>().LootCobra[1])
            {
                AparaceBotoesVenda[0].SetActive(true); //Botao de vender reaparece
            }
        }
        if (val == 3)
        {
            Escolhido = 0.4f;
            Itens = 0;
            ChangingText[0].text = "" + Itens;
            Grana = 0;
            ChangingText[1].text = "" + Grana;
            if (Itens <= ControleDeItens.GetComponent<MenuDeLoot>().LootAranha[0])
            {
                AparaceBotoesVenda[0].SetActive(true); //Botao de vender reaparece
            }
        }
        if (val == 4)
        {
            Escolhido = 0.5f;
            Itens = 0;
            ChangingText[0].text = "" + Itens;
            Grana = 0;
            ChangingText[1].text = "" + Grana;
            if (Itens <= ControleDeItens.GetComponent<MenuDeLoot>().LootAranha[1])
            {
                AparaceBotoesVenda[0].SetActive(true); //Botao de vender reaparece
            }
        }
        if (val == 5)
        {
            Escolhido = 0.6f;
            Itens = 0;
            ChangingText[0].text = "" + Itens;
            Grana = 0;
            ChangingText[1].text = "" + Grana;
            if (Itens <= ControleDeItens.GetComponent<MenuDeLoot>().LootAbelha[0])
            {
                AparaceBotoesVenda[0].SetActive(true); //Botao de vender reaparece
            }
        }
        if (val == 6)
        {
            Escolhido = 0.7f;
            Itens = 0;
            ChangingText[0].text = "" + Itens;
            Grana = 0;
            ChangingText[1].text = "" + Grana;
            if (Itens <= ControleDeItens.GetComponent<MenuDeLoot>().LootAbelha[1])
            {
                AparaceBotoesVenda[0].SetActive(true); //Botao de vender reaparece
            }
        }
        if (val == 7)
        {
            Escolhido = 0.8f;
            Itens = 0;
            ChangingText[0].text = "" + Itens;
            Grana = 0;
            ChangingText[1].text = "" + Grana;
            if (Itens <= ControleDeItens.GetComponent<MenuDeLoot>().LootAbelha[2])
            {
                AparaceBotoesVenda[0].SetActive(true); //Botao de vender reaparece
            }
        }
        if (val == 8)
        {
            Escolhido = 0.9f;
            Itens = 0;
            ChangingText[0].text = "" + Itens;
            Grana = 0;
            ChangingText[1].text = "" + Grana;
            if (Itens <= ControleDeItens.GetComponent<MenuDeLoot>().LootEsqueleto[0])
            {
                AparaceBotoesVenda[0].SetActive(true); //Botao de vender reaparece
            }
        }
        if (val == 9)
        {
            Escolhido = 0.11f;
            Itens = 0;
            ChangingText[0].text = "" + Itens;
            Grana = 0;
            ChangingText[1].text = "" + Grana;
            if (Itens <= ControleDeItens.GetComponent<MenuDeLoot>().LootEsqueleto[1])
            {
                AparaceBotoesVenda[0].SetActive(true); //Botao de vender reaparece
            }
        }
        if (val == 10)
        {
            Escolhido = 0.12f;
            Itens = 0;
            ChangingText[0].text = "" + Itens;
            Grana = 0;
            ChangingText[1].text = "" + Grana;
            if (Itens <= ControleDeItens.GetComponent<MenuDeLoot>().LootCobraIrritada[0])
            {
                AparaceBotoesVenda[0].SetActive(true); //Botao de vender reaparece
            }
        }
        if (val == 11)
        {
            Escolhido = 0.13f;
            Itens = 0;
            ChangingText[0].text = "" + Itens;
            Grana = 0;
            ChangingText[1].text = "" + Grana;
            if (Itens <= ControleDeItens.GetComponent<MenuDeLoot>().LootCobraIrritada[1])
            {
                AparaceBotoesVenda[0].SetActive(true); //Botao de vender reaparece
            }
        }
        if (val == 12)
        {
            Escolhido = 0.14f;
            Itens = 0;
            ChangingText[0].text = "" + Itens;
            Grana = 0;
            ChangingText[1].text = "" + Grana;
            if (Itens <= ControleDeItens.GetComponent<MenuDeLoot>().LootDragon[0])
            {
                AparaceBotoesVenda[0].SetActive(true); //Botao de vender reaparece
            }
        }
        if (val == 13)
        {
            Escolhido = 0.15f;
            Itens = 0;
            ChangingText[0].text = "" + Itens;
            Grana = 0;
            ChangingText[1].text = "" + Grana;
            if (Itens <= ControleDeItens.GetComponent<MenuDeLoot>().LootDragon[1])
            {
                AparaceBotoesVenda[0].SetActive(true); //Botao de vender reaparece
            }
        }
        if (val == 14)
        {
            Escolhido = 0.16f;
            Itens = 0;
            ChangingText[0].text = "" + Itens;
            Grana = 0;
            ChangingText[1].text = "" + Grana;
            if (Itens <= ControleDeItens.GetComponent<MenuDeLoot>().Essencias[0])
            {
                AparaceBotoesVenda[0].SetActive(true); //Botao de vender reaparece
            }
        }
        if (val == 15)
        {
            Escolhido = 0.17f;
            Itens = 0;
            ChangingText[0].text = "" + Itens;
            Grana = 0;
            ChangingText[1].text = "" + Grana;
            if (Itens <= ControleDeItens.GetComponent<MenuDeLoot>().Essencias[1])
            {
                AparaceBotoesVenda[0].SetActive(true); //Botao de vender reaparece
            }
        }
    }
    
    // AUMENTA ITENS +++++++++
    public void AumantaQuantidade()
    {
        if (Escolhido == 0.0f)
        {
            Grana = 0;
            ChangingText[1].text = "" + Grana;
            Itens = 0;
            ChangingText[0].text = "" + Itens;
            AparaceBotoesVenda[0].SetActive(false); //Botao de vender desaparece         
        }
        if (Escolhido == 0.2f)
        {
            Grana = Grana + 1;
            ChangingText[1].text = "" + Grana;
            Itens++;
            ChangingText[0].text = "" + Itens;
            if (Itens > ControleDeItens.GetComponent<MenuDeLoot>().LootCobra[0]) {
                AparaceBotoesVenda[0].SetActive(false); //Botao de vender desaparece
            }
        }
        if (Escolhido == 0.3f)
        {
            Grana = Grana + 2;
            ChangingText[1].text = "" + Grana;
            Itens++;
            ChangingText[0].text = "" + Itens;
            if (Itens > ControleDeItens.GetComponent<MenuDeLoot>().LootCobra[1])
            {
                AparaceBotoesVenda[0].SetActive(false); //Botao de vender desaparece
            }
        }
        if (Escolhido == 0.4f)
        {
            Grana = Grana + 2;
            ChangingText[1].text = "" + Grana;
            Itens++;
            ChangingText[0].text = "" + Itens;
            if (Itens > ControleDeItens.GetComponent<MenuDeLoot>().LootAranha[0])
            {
                AparaceBotoesVenda[0].SetActive(false); //Botao de vender desaparece
            }
        }
        if (Escolhido == 0.5f)
        {
            Grana = Grana + 2;
            ChangingText[1].text = "" + Grana;
            Itens++;
            ChangingText[0].text = "" + Itens;
            if (Itens > ControleDeItens.GetComponent<MenuDeLoot>().LootAranha[1])
            {
                AparaceBotoesVenda[0].SetActive(false); //Botao de vender desaparece
            }
        }
        if (Escolhido == 0.6f)
        {
            Grana = Grana + 3;
            ChangingText[1].text = "" + Grana;
            Itens++;
            ChangingText[0].text = "" + Itens;
            if (Itens > ControleDeItens.GetComponent<MenuDeLoot>().LootAbelha[0])
            {
                AparaceBotoesVenda[0].SetActive(false); //Botao de vender desaparece
            }
        }
        if (Escolhido == 0.7f)
        {
            Grana = Grana + 5;
            ChangingText[1].text = "" + Grana;
            Itens++;
            ChangingText[0].text = "" + Itens;
            if (Itens > ControleDeItens.GetComponent<MenuDeLoot>().LootAbelha[1])
            {
                AparaceBotoesVenda[0].SetActive(false); //Botao de vender desaparece
            }
        }
        if (Escolhido == 0.8f)
        {
            Grana = Grana + 10;
            ChangingText[1].text = "" + Grana;
            Itens++;
            ChangingText[0].text = "" + Itens;
            if (Itens > ControleDeItens.GetComponent<MenuDeLoot>().LootAbelha[2])
            {
                AparaceBotoesVenda[0].SetActive(false); //Botao de vender desaparece
            }
        }
        if (Escolhido == 0.9f)
        {
            Grana = Grana + 8;
            ChangingText[1].text = "" + Grana;
            Itens++;
            ChangingText[0].text = "" + Itens;
            if (Itens > ControleDeItens.GetComponent<MenuDeLoot>().LootEsqueleto[0])
            {
                AparaceBotoesVenda[0].SetActive(false); //Botao de vender desaparece
            }
        }
        if (Escolhido == 0.11f)
        {
            Grana = Grana + 8;
            ChangingText[1].text = "" + Grana;
            Itens++;
            ChangingText[0].text = "" + Itens;
            if (Itens > ControleDeItens.GetComponent<MenuDeLoot>().LootEsqueleto[1])
            {
                AparaceBotoesVenda[0].SetActive(false); //Botao de vender desaparece
            }
        }
        if (Escolhido == 0.12f)
        {
            Grana = Grana + 10;
            ChangingText[1].text = "" + Grana;
            Itens++;
            ChangingText[0].text = "" + Itens;
            if (Itens > ControleDeItens.GetComponent<MenuDeLoot>().LootCobraIrritada[0])
            {
                AparaceBotoesVenda[0].SetActive(false); //Botao de vender desaparece
            }
        }
        if (Escolhido == 0.13f)
        {
            Grana = Grana + 10;
            ChangingText[1].text = "" + Grana;
            Itens++;
            ChangingText[0].text = "" + Itens;
            if (Itens > ControleDeItens.GetComponent<MenuDeLoot>().LootCobraIrritada[1])
            {
                AparaceBotoesVenda[0].SetActive(false); //Botao de vender desaparece
            }
        }
        if (Escolhido == 0.14f)
        {
            Grana = Grana + 15;
            ChangingText[1].text = "" + Grana;
            Itens++;
            ChangingText[0].text = "" + Itens;
            if (Itens > ControleDeItens.GetComponent<MenuDeLoot>().LootDragon[0])
            {
                AparaceBotoesVenda[0].SetActive(false); //Botao de vender desaparece
            }
        }
        if (Escolhido == 0.15f)
        {
            Grana = Grana + 25;
            ChangingText[1].text = "" + Grana;
            Itens++;
            ChangingText[0].text = "" + Itens;
            if (Itens > ControleDeItens.GetComponent<MenuDeLoot>().LootDragon[1])
            {
                AparaceBotoesVenda[0].SetActive(false); //Botao de vender desaparece
            }
        }
        if (Escolhido == 0.16f) //TERRA
        {
            Grana = Grana + 10;
            ChangingText[1].text = "" + Grana;
            Itens++;
            ChangingText[0].text = "" + Itens;
            if (Itens > ControleDeItens.GetComponent<MenuDeLoot>().Essencias[0])
            {
                AparaceBotoesVenda[0].SetActive(false); //Botao de vender desaparece
            }
        }
        if (Escolhido == 0.17f) //FOGO
        {
            Grana = Grana + 15;
            ChangingText[1].text = "" + Grana;
            Itens++;
            ChangingText[0].text = "" + Itens;
            if (Itens > ControleDeItens.GetComponent<MenuDeLoot>().Essencias[1])
            {
                AparaceBotoesVenda[0].SetActive(false); //Botao de vender desaparece
            }
        }
    }
    // DIMINUI ITENS ----------
    public void ReduzQuantidade()
    {
        if (Itens > 0) {

            Itens--;
            ChangingText[0].text = "" + Itens;
        }
        if (Escolhido == 0.0f)
        {
            Grana = 0;
            ChangingText[1].text = "" + Grana;
            Itens = 0;
            ChangingText[0].text = "" + Itens;
        }
        if (Escolhido == 0.2f && Grana > 0)
        {
            Grana = Grana - 1;
            ChangingText[1].text = "" + Grana;
            if (Itens <= ControleDeItens.GetComponent<MenuDeLoot>().LootCobra[0])
            {
                AparaceBotoesVenda[0].SetActive(true); //Botao de vender reaparece
            }
        }
        if (Escolhido == 0.3f && Grana > 0)
        {
            Grana = Grana - 2;
            ChangingText[1].text = "" + Grana;
            if (Itens <= ControleDeItens.GetComponent<MenuDeLoot>().LootCobra[1])
            {
                AparaceBotoesVenda[0].SetActive(true); //Botao de vender reaparece
            }
        }
        if (Escolhido == 0.4f && Grana > 0)
        {
            Grana = Grana - 2;
            ChangingText[1].text = "" + Grana;
            if (Itens <= ControleDeItens.GetComponent<MenuDeLoot>().LootAranha[0])
            {
                AparaceBotoesVenda[0].SetActive(true); //Botao de vender reaparece
            }
        }
        if (Escolhido == 0.5f && Grana > 0)
        {
            Grana = Grana - 2;
            ChangingText[1].text = "" + Grana;
            if (Itens <= ControleDeItens.GetComponent<MenuDeLoot>().LootAranha[1])
            {
                AparaceBotoesVenda[0].SetActive(true); //Botao de vender reaparece
            }
        }
        if (Escolhido == 0.6f && Grana > 0)
        {
            Grana = Grana - 3;
            ChangingText[1].text = "" + Grana;
            if (Itens <= ControleDeItens.GetComponent<MenuDeLoot>().LootAbelha[0])
            {
                AparaceBotoesVenda[0].SetActive(true); //Botao de vender reaparece
            }
        }
        if (Escolhido == 0.7f && Grana > 0)
        {
            Grana = Grana - 5;
            ChangingText[1].text = "" + Grana;
            if (Itens <= ControleDeItens.GetComponent<MenuDeLoot>().LootAbelha[1])
            {
                AparaceBotoesVenda[0].SetActive(true); //Botao de vender reaparece
            }
        }
        if (Escolhido == 0.8f && Grana > 0)
        {
            Grana = Grana - 10;
            ChangingText[1].text = "" + Grana;
            if (Itens <= ControleDeItens.GetComponent<MenuDeLoot>().LootAbelha[2])
            {
                AparaceBotoesVenda[0].SetActive(true); //Botao de vender reaparece
            }
        }
        if (Escolhido == 0.9f && Grana > 0)
        {
            Grana = Grana - 8;
            ChangingText[1].text = "" + Grana;
            if (Itens <= ControleDeItens.GetComponent<MenuDeLoot>().LootEsqueleto[0])
            {
                AparaceBotoesVenda[0].SetActive(true); //Botao de vender reaparece
            }
        }
        if (Escolhido == 0.11f && Grana > 0)
        {
            Grana = Grana - 8;
            ChangingText[1].text = "" + Grana;
            if (Itens <= ControleDeItens.GetComponent<MenuDeLoot>().LootEsqueleto[1])
            {
                AparaceBotoesVenda[0].SetActive(true); //Botao de vender reaparece
            }
        }
        if (Escolhido == 0.12f && Grana > 0)
        {
            Grana = Grana - 10;
            ChangingText[1].text = "" + Grana;
            if (Itens <= ControleDeItens.GetComponent<MenuDeLoot>().LootCobraIrritada[0])
            {
                AparaceBotoesVenda[0].SetActive(true); //Botao de vender reaparece
            }
        }
        if (Escolhido == 0.13f && Grana > 0)
        {
            Grana = Grana - 10;
            ChangingText[1].text = "" + Grana;
            if (Itens <= ControleDeItens.GetComponent<MenuDeLoot>().LootCobraIrritada[1])
            {
                AparaceBotoesVenda[0].SetActive(true); //Botao de vender reaparece
            }
        }
        if (Escolhido == 0.14f && Grana > 0)
        {
            Grana = Grana - 15;
            ChangingText[1].text = "" + Grana;
            if (Itens <= ControleDeItens.GetComponent<MenuDeLoot>().LootDragon[0])
            {
                AparaceBotoesVenda[0].SetActive(true); //Botao de vender reaparece
            }
        }
        if (Escolhido == 0.15f && Grana > 0)
        {
            Grana = Grana - 25;
            ChangingText[1].text = "" + Grana;
            if (Itens <= ControleDeItens.GetComponent<MenuDeLoot>().LootDragon[1])
            {
                AparaceBotoesVenda[0].SetActive(true); //Botao de vender reaparece
            }
        }
        if (Escolhido == 0.16f && Grana > 0)
        {
            Grana = Grana - 10;
            ChangingText[1].text = "" + Grana;
            if (Itens <= ControleDeItens.GetComponent<MenuDeLoot>().Essencias[0])
            {
                AparaceBotoesVenda[0].SetActive(true); //Botao de vender reaparece
            }
        }
        if (Escolhido == 0.17f && Grana > 0)
        {
            Grana = Grana - 15;
            ChangingText[1].text = "" + Grana;
            if (Itens <= ControleDeItens.GetComponent<MenuDeLoot>().Essencias[1])
            {
                AparaceBotoesVenda[0].SetActive(true); //Botao de vender reaparece
            }
        }
    }
    // VENDE O ITENS ========
    public void SellThis()
    {
        if (Escolhido == 0.2f)
        {
            ControleDeItens.GetComponent<MenuDeLoot>().LootCobra[0] = ControleDeItens.GetComponent<MenuDeLoot>().LootCobra[0] - Itens;
            Boy.GetComponentInChildren<LevelBoy>().Moedas = Boy.GetComponentInChildren<LevelBoy>().Moedas + Grana;
            Boy.GetComponentInChildren<BoyHP>().SaveInfoBoyHP();
            Boy.GetComponentInChildren<AtributosBoy>().SaveInfoBoyAtributos();
            Boy.GetComponentInChildren<WeaponManager>().SaveInfoBoyWEAPONS();
            Boy.GetComponentInChildren<LevelBoy>().SaveInfoBoyLEVEL();
            Boy.GetComponentInChildren<MissoesManager>().SaveInfoBoyMISSION();
            ControleDeItens.GetComponent<MenuDeLoot>().SaveInfoBoyLOOTS();

            ControleDeItens.GetComponent<MenuDeLoot>().AtualizarRecursos();
        }
        if (Escolhido == 0.3f)
        {
            ControleDeItens.GetComponent<MenuDeLoot>().LootCobra[1] = ControleDeItens.GetComponent<MenuDeLoot>().LootCobra[1] - Itens;
            Boy.GetComponentInChildren<LevelBoy>().Moedas = Boy.GetComponentInChildren<LevelBoy>().Moedas + Grana;
            Boy.GetComponentInChildren<BoyHP>().SaveInfoBoyHP();
            Boy.GetComponentInChildren<AtributosBoy>().SaveInfoBoyAtributos();
            Boy.GetComponentInChildren<WeaponManager>().SaveInfoBoyWEAPONS();
            Boy.GetComponentInChildren<LevelBoy>().SaveInfoBoyLEVEL();
            Boy.GetComponentInChildren<MissoesManager>().SaveInfoBoyMISSION();
            ControleDeItens.GetComponent<MenuDeLoot>().SaveInfoBoyLOOTS();

            ControleDeItens.GetComponent<MenuDeLoot>().AtualizarRecursos();
        }
        if (Escolhido == 0.4f)
        {
            ControleDeItens.GetComponent<MenuDeLoot>().LootAranha[0] = ControleDeItens.GetComponent<MenuDeLoot>().LootAranha[0] - Itens;
            Boy.GetComponentInChildren<LevelBoy>().Moedas = Boy.GetComponentInChildren<LevelBoy>().Moedas + Grana;
            Boy.GetComponentInChildren<BoyHP>().SaveInfoBoyHP();
            Boy.GetComponentInChildren<AtributosBoy>().SaveInfoBoyAtributos();
            Boy.GetComponentInChildren<WeaponManager>().SaveInfoBoyWEAPONS();
            Boy.GetComponentInChildren<LevelBoy>().SaveInfoBoyLEVEL();
            Boy.GetComponentInChildren<MissoesManager>().SaveInfoBoyMISSION();
            ControleDeItens.GetComponent<MenuDeLoot>().SaveInfoBoyLOOTS();

            ControleDeItens.GetComponent<MenuDeLoot>().AtualizarRecursos();
        }
        if (Escolhido == 0.5f)
        {
            ControleDeItens.GetComponent<MenuDeLoot>().LootAranha[1] = ControleDeItens.GetComponent<MenuDeLoot>().LootAranha[1] - Itens;
            Boy.GetComponentInChildren<LevelBoy>().Moedas = Boy.GetComponentInChildren<LevelBoy>().Moedas + Grana;
            Boy.GetComponentInChildren<BoyHP>().SaveInfoBoyHP();
            Boy.GetComponentInChildren<AtributosBoy>().SaveInfoBoyAtributos();
            Boy.GetComponentInChildren<WeaponManager>().SaveInfoBoyWEAPONS();
            Boy.GetComponentInChildren<LevelBoy>().SaveInfoBoyLEVEL();
            Boy.GetComponentInChildren<MissoesManager>().SaveInfoBoyMISSION();
            ControleDeItens.GetComponent<MenuDeLoot>().SaveInfoBoyLOOTS();

            ControleDeItens.GetComponent<MenuDeLoot>().AtualizarRecursos();
        }
        if (Escolhido == 0.6f)
        {
            ControleDeItens.GetComponent<MenuDeLoot>().LootAbelha[0] = ControleDeItens.GetComponent<MenuDeLoot>().LootAbelha[0] - Itens;
            Boy.GetComponentInChildren<LevelBoy>().Moedas = Boy.GetComponentInChildren<LevelBoy>().Moedas + Grana;
            Boy.GetComponentInChildren<BoyHP>().SaveInfoBoyHP();
            Boy.GetComponentInChildren<AtributosBoy>().SaveInfoBoyAtributos();
            Boy.GetComponentInChildren<WeaponManager>().SaveInfoBoyWEAPONS();
            Boy.GetComponentInChildren<LevelBoy>().SaveInfoBoyLEVEL();
            Boy.GetComponentInChildren<MissoesManager>().SaveInfoBoyMISSION();
            ControleDeItens.GetComponent<MenuDeLoot>().SaveInfoBoyLOOTS();

            ControleDeItens.GetComponent<MenuDeLoot>().AtualizarRecursos();
        }
        if (Escolhido == 0.7f)
        {
            ControleDeItens.GetComponent<MenuDeLoot>().LootAbelha[1] = ControleDeItens.GetComponent<MenuDeLoot>().LootAbelha[1] - Itens;
            Boy.GetComponentInChildren<LevelBoy>().Moedas = Boy.GetComponentInChildren<LevelBoy>().Moedas + Grana;
            Boy.GetComponentInChildren<BoyHP>().SaveInfoBoyHP();
            Boy.GetComponentInChildren<AtributosBoy>().SaveInfoBoyAtributos();
            Boy.GetComponentInChildren<WeaponManager>().SaveInfoBoyWEAPONS();
            Boy.GetComponentInChildren<LevelBoy>().SaveInfoBoyLEVEL();
            Boy.GetComponentInChildren<MissoesManager>().SaveInfoBoyMISSION();
            ControleDeItens.GetComponent<MenuDeLoot>().SaveInfoBoyLOOTS();

            ControleDeItens.GetComponent<MenuDeLoot>().AtualizarRecursos();
        }
        if (Escolhido == 0.8f)
        {
            ControleDeItens.GetComponent<MenuDeLoot>().LootAbelha[2] = ControleDeItens.GetComponent<MenuDeLoot>().LootAbelha[2] - Itens;
            Boy.GetComponentInChildren<LevelBoy>().Moedas = Boy.GetComponentInChildren<LevelBoy>().Moedas + Grana;
            Boy.GetComponentInChildren<BoyHP>().SaveInfoBoyHP();
            Boy.GetComponentInChildren<AtributosBoy>().SaveInfoBoyAtributos();
            Boy.GetComponentInChildren<WeaponManager>().SaveInfoBoyWEAPONS();
            Boy.GetComponentInChildren<LevelBoy>().SaveInfoBoyLEVEL();
            Boy.GetComponentInChildren<MissoesManager>().SaveInfoBoyMISSION();
            ControleDeItens.GetComponent<MenuDeLoot>().SaveInfoBoyLOOTS();

            ControleDeItens.GetComponent<MenuDeLoot>().AtualizarRecursos();
        }
        if (Escolhido == 0.9f)
        {
            ControleDeItens.GetComponent<MenuDeLoot>().LootEsqueleto[0] = ControleDeItens.GetComponent<MenuDeLoot>().LootEsqueleto[0] - Itens;
            Boy.GetComponentInChildren<LevelBoy>().Moedas = Boy.GetComponentInChildren<LevelBoy>().Moedas + Grana;
            Boy.GetComponentInChildren<BoyHP>().SaveInfoBoyHP();
            Boy.GetComponentInChildren<AtributosBoy>().SaveInfoBoyAtributos();
            Boy.GetComponentInChildren<WeaponManager>().SaveInfoBoyWEAPONS();
            Boy.GetComponentInChildren<LevelBoy>().SaveInfoBoyLEVEL();
            Boy.GetComponentInChildren<MissoesManager>().SaveInfoBoyMISSION();
            ControleDeItens.GetComponent<MenuDeLoot>().SaveInfoBoyLOOTS();

            ControleDeItens.GetComponent<MenuDeLoot>().AtualizarRecursos();
        }
        if (Escolhido == 0.11f)
        {
            ControleDeItens.GetComponent<MenuDeLoot>().LootEsqueleto[1] = ControleDeItens.GetComponent<MenuDeLoot>().LootEsqueleto[1] - Itens;
            Boy.GetComponentInChildren<LevelBoy>().Moedas = Boy.GetComponentInChildren<LevelBoy>().Moedas + Grana;
            Boy.GetComponentInChildren<BoyHP>().SaveInfoBoyHP();
            Boy.GetComponentInChildren<AtributosBoy>().SaveInfoBoyAtributos();
            Boy.GetComponentInChildren<WeaponManager>().SaveInfoBoyWEAPONS();
            Boy.GetComponentInChildren<LevelBoy>().SaveInfoBoyLEVEL();
            Boy.GetComponentInChildren<MissoesManager>().SaveInfoBoyMISSION();
            ControleDeItens.GetComponent<MenuDeLoot>().SaveInfoBoyLOOTS();

            ControleDeItens.GetComponent<MenuDeLoot>().AtualizarRecursos();
        }
        if (Escolhido == 0.12f)
        {
            ControleDeItens.GetComponent<MenuDeLoot>().LootCobraIrritada[0] = ControleDeItens.GetComponent<MenuDeLoot>().LootCobraIrritada[0] - Itens;
            Boy.GetComponentInChildren<LevelBoy>().Moedas = Boy.GetComponentInChildren<LevelBoy>().Moedas + Grana;
            Boy.GetComponentInChildren<BoyHP>().SaveInfoBoyHP();
            Boy.GetComponentInChildren<AtributosBoy>().SaveInfoBoyAtributos();
            Boy.GetComponentInChildren<WeaponManager>().SaveInfoBoyWEAPONS();
            Boy.GetComponentInChildren<LevelBoy>().SaveInfoBoyLEVEL();
            Boy.GetComponentInChildren<MissoesManager>().SaveInfoBoyMISSION();
            ControleDeItens.GetComponent<MenuDeLoot>().SaveInfoBoyLOOTS();

            ControleDeItens.GetComponent<MenuDeLoot>().AtualizarRecursos();
        }
        if (Escolhido == 0.13f)
        {
            ControleDeItens.GetComponent<MenuDeLoot>().LootCobraIrritada[1] = ControleDeItens.GetComponent<MenuDeLoot>().LootCobraIrritada[1] - Itens;
            Boy.GetComponentInChildren<LevelBoy>().Moedas = Boy.GetComponentInChildren<LevelBoy>().Moedas + Grana;
            Boy.GetComponentInChildren<BoyHP>().SaveInfoBoyHP();
            Boy.GetComponentInChildren<AtributosBoy>().SaveInfoBoyAtributos();
            Boy.GetComponentInChildren<WeaponManager>().SaveInfoBoyWEAPONS();
            Boy.GetComponentInChildren<LevelBoy>().SaveInfoBoyLEVEL();
            Boy.GetComponentInChildren<MissoesManager>().SaveInfoBoyMISSION();
            ControleDeItens.GetComponent<MenuDeLoot>().SaveInfoBoyLOOTS();

            ControleDeItens.GetComponent<MenuDeLoot>().AtualizarRecursos();
        }
        if (Escolhido == 0.14f)
        {
            ControleDeItens.GetComponent<MenuDeLoot>().LootDragon[0] = ControleDeItens.GetComponent<MenuDeLoot>().LootDragon[0] - Itens;
            Boy.GetComponentInChildren<LevelBoy>().Moedas = Boy.GetComponentInChildren<LevelBoy>().Moedas + Grana;
            Boy.GetComponentInChildren<BoyHP>().SaveInfoBoyHP();
            Boy.GetComponentInChildren<AtributosBoy>().SaveInfoBoyAtributos();
            Boy.GetComponentInChildren<WeaponManager>().SaveInfoBoyWEAPONS();
            Boy.GetComponentInChildren<LevelBoy>().SaveInfoBoyLEVEL();
            Boy.GetComponentInChildren<MissoesManager>().SaveInfoBoyMISSION();
            ControleDeItens.GetComponent<MenuDeLoot>().SaveInfoBoyLOOTS();

            ControleDeItens.GetComponent<MenuDeLoot>().AtualizarRecursos();
        }
        if (Escolhido == 0.15f)
        {
            ControleDeItens.GetComponent<MenuDeLoot>().LootDragon[1] = ControleDeItens.GetComponent<MenuDeLoot>().LootDragon[1] - Itens;
            Boy.GetComponentInChildren<LevelBoy>().Moedas = Boy.GetComponentInChildren<LevelBoy>().Moedas + Grana;
            Boy.GetComponentInChildren<BoyHP>().SaveInfoBoyHP();
            Boy.GetComponentInChildren<AtributosBoy>().SaveInfoBoyAtributos();
            Boy.GetComponentInChildren<WeaponManager>().SaveInfoBoyWEAPONS();
            Boy.GetComponentInChildren<LevelBoy>().SaveInfoBoyLEVEL();
            Boy.GetComponentInChildren<MissoesManager>().SaveInfoBoyMISSION();
            ControleDeItens.GetComponent<MenuDeLoot>().SaveInfoBoyLOOTS();

            ControleDeItens.GetComponent<MenuDeLoot>().AtualizarRecursos();
        }
        if (Escolhido == 0.16f)
        {
            ControleDeItens.GetComponent<MenuDeLoot>().Essencias[0] = ControleDeItens.GetComponent<MenuDeLoot>().Essencias[0] - Itens;
            Boy.GetComponentInChildren<LevelBoy>().Moedas = Boy.GetComponentInChildren<LevelBoy>().Moedas + Grana;
            Boy.GetComponentInChildren<BoyHP>().SaveInfoBoyHP();
            Boy.GetComponentInChildren<AtributosBoy>().SaveInfoBoyAtributos();
            Boy.GetComponentInChildren<WeaponManager>().SaveInfoBoyWEAPONS();
            Boy.GetComponentInChildren<LevelBoy>().SaveInfoBoyLEVEL();
            Boy.GetComponentInChildren<MissoesManager>().SaveInfoBoyMISSION();
            ControleDeItens.GetComponent<MenuDeLoot>().SaveInfoBoyLOOTS();

            ControleDeItens.GetComponent<MenuDeLoot>().AtualizarRecursos();
        }
        if (Escolhido == 0.17f)
        {
            ControleDeItens.GetComponent<MenuDeLoot>().Essencias[1] = ControleDeItens.GetComponent<MenuDeLoot>().Essencias[1] - Itens;
            Boy.GetComponentInChildren<LevelBoy>().Moedas = Boy.GetComponentInChildren<LevelBoy>().Moedas + Grana;
            Boy.GetComponentInChildren<BoyHP>().SaveInfoBoyHP();
            Boy.GetComponentInChildren<AtributosBoy>().SaveInfoBoyAtributos();
            Boy.GetComponentInChildren<WeaponManager>().SaveInfoBoyWEAPONS();
            Boy.GetComponentInChildren<LevelBoy>().SaveInfoBoyLEVEL();
            Boy.GetComponentInChildren<MissoesManager>().SaveInfoBoyMISSION();
            ControleDeItens.GetComponent<MenuDeLoot>().SaveInfoBoyLOOTS();

            ControleDeItens.GetComponent<MenuDeLoot>().AtualizarRecursos();
        }
    }
    //***********************************************************************************************************************************

    //Quarta Imagem de "troca efetuada com sucesso"
    public void AbreImagem4()
    {
        Imagens[0].SetActive(false);
        Imagens[1].SetActive(false);
        Imagens[2].SetActive(false);
        Imagens[3].SetActive(true);
    }
}
