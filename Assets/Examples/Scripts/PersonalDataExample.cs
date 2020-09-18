using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PersonalDataExample : MonoBehaviour
{
    public string dataCsvFileName = "";
    public GameObject textObjectPrefab = null;

    List<Person> _people = new List<Person>();
    Dictionary<int, GameObject> _mainObjectLookup = new Dictionary<int, GameObject>();
    int _ageMin, _ageMax;

    void Awake()
    {
        // Parse.
        string csvFilePath = Application.streamingAssetsPath + "/" + dataCsvFileName;
        string csvContent = File.ReadAllText(csvFilePath);
        Parse(csvContent);

        // filter.
        Filter();

        // Mine.
        Mine();

        // Represent.
        Represent();

        //Interaction
        AddInteraction();


        Debug.Log("_people.count: " + _people.Count);
    }
    void Parse(string csvText)
    {
        // Split by new lines in rows.
        string[] rowContents = csvText.Split('\n');
        // For each row.
        for (int r = 1; r < rowContents.Length; r++)
        {
            string rowContent = rowContents[r];
            string[] fieldContents = rowContent.Split(',');
            Person person = new Person(r);
            // For each field in this row.
            for (int f = 0; f < fieldContents.Length; f++)
            {
                string fieldContent = fieldContents[f];
                switch (f)
                {
                    case 0:
                        // First name.
                        person.firstName = fieldContent;
                        break;
                    case 1:
                        // Last name.
                        person.lastName = fieldContent;
                        break;
                    case 2:
                        // Age.
                        int age;
                        bool parseSucceeded = int.TryParse(fieldContent, out age);
                        if (parseSucceeded) person.age = age;
                        break;
                    case 3:
                        // Had covid
                        person.hadCovid = fieldContent.ToLower() == "yes";
                        break;
                    // case 7:
                        // Postnummer
                        // int postnummer;
                        // bool parseSucceeded = int.TryParse(fieldContent, out postnummer);
                        // if (parseSucceeded) person.postNumber = postnummer;
                        // break;
                }
            }
            // Parse covid relation level.
            Person.CovidRelationLevel covidRelationLevel = Person.CovidRelationLevel.None;
            if (fieldContents.Length > 6)
            {
                bool familyHadCovid = fieldContents[4].ToLower() == "yes";
                bool familyOrFriendsHadCovid = fieldContents[5].ToLower() == "yes";
                bool anyoneHadCovid = fieldContents[6].ToLower() == "yes";
                if (familyHadCovid) covidRelationLevel = Person.CovidRelationLevel.Family;
                else if (familyOrFriendsHadCovid) covidRelationLevel = Person.CovidRelationLevel.FamilyOrFriend;
                else if (anyoneHadCovid) covidRelationLevel = Person.CovidRelationLevel.Anyone;
            }
            person.covidRelationLevel = covidRelationLevel;

            //Add to person list.
            _people.Add(person);
        }
    }

    void Filter()
    {
        for( int p = _people.Count-1; p >= 0; p-- )
        {
            Person person = _people[p];

            if(person.age < 18 || person.age > 127) { //If too young or (||) too old. 
                _people.RemoveAt(p);
                Debug.Log(person.firstName + " is " + "invalid");
            }
            
        }
    }
    
    void Mine()
    {
        _ageMin = int.MaxValue;
        _ageMax = int.MinValue;
        foreach( Person person in _people)
        {
            if (person.age > _ageMax) _ageMax = person.age;
            else if (person.age < _ageMin) _ageMin = person.age;
        }
        Debug.Log( "Min: " + _ageMin + ", Max: " + _ageMax);
    }
    

    void Represent()
    {
        // sort by age
        _people.Sort( (a, b ) => a.age - b.age );

        // create elements

        for( int p = 0; p < _people.Count; p++)
        {
            Person person = _people[p];

            float mainX = p;
            float barHeight = Mathf.InverseLerp( 0, _ageMax, person.age) * 10; //rescale to fit between 0 and 10 and define 10 as the max age
            float barY = barHeight * 0.5f;
            float barwidth = 0.9f;

            GameObject mainObject = new GameObject(person.id + " " + person.firstName );
            mainObject.transform.SetParent(transform);
            mainObject.transform.localPosition = new Vector3(mainX, 0, 0);

            GameObject barObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            barObject.transform.SetParent(mainObject.transform);
            barObject.transform.localPosition = new Vector3(0, barY, 0);
            barObject.transform.localScale = new Vector3(barwidth, barHeight, 1);

            _mainObjectLookup.Add(person.id, mainObject);
        }
    }

    void AddInteraction()
    {

        foreach( Person person in _people )
        {
            GameObject mainObject = _mainObjectLookup[person.id];

            GameObject textObject = Instantiate(textObjectPrefab); //make a copy of original dummy
            textObject.transform.SetParent(mainObject.transform);
            textObject.SetActive(true);
            textObject.transform.localPosition = Vector3.zero;
            textObject.transform.Rotate(0, 0, -45);
            textObject.GetComponent<TextMesh>().text = person.firstName;

            Collider barCollider = mainObject.GetComponentInChildren<Collider>();
            TextRevealer textRevealer = barCollider.gameObject.AddComponent<TextRevealer>();  //instantiate a script and add it to colliders gameObject.
            textRevealer.textObject = textObject;
            
        }
    }




}