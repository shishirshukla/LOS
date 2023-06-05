using System.Xml.Serialization;

namespace MobileBackend.Models.PAN
{
	

	[XmlRoot(ElementName = "Organization")]
	public class Organization
	{

		

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "code")]
		public string Code { get; set; }

		[XmlAttribute(AttributeName = "tin")]
		public string Tin { get; set; }

		[XmlAttribute(AttributeName = "uid")]
		public string Uid { get; set; }

		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }
	}

	[XmlRoot(ElementName = "IssuedBy")]
	public class IssuedBy
	{

		[XmlElement(ElementName = "Organization")]
		public Organization Organization { get; set; }
	}

	[XmlRoot(ElementName = "Photo")]
	public class Photo
	{

		[XmlAttribute(AttributeName = "format")]
		public string Format { get; set; }
	}

	[XmlRoot(ElementName = "Person")]
	public class Person
	{

		

		

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "dob")]
		public string Dob { get; set; }

		[XmlAttribute(AttributeName = "swd")]
		public string Swd { get; set; }

		[XmlAttribute(AttributeName = "swdIndicator")]
		public string SwdIndicator { get; set; }

		[XmlAttribute(AttributeName = "gender")]
		public string Gender { get; set; }

		[XmlAttribute(AttributeName = "maritalStatus")]
		public string MaritalStatus { get; set; }

		[XmlAttribute(AttributeName = "religion")]
		public string Religion { get; set; }

		[XmlAttribute(AttributeName = "phone")]
		public string Phone { get; set; }

		[XmlAttribute(AttributeName = "email")]
		public string Email { get; set; }
	}

	[XmlRoot(ElementName = "IssuedTo")]
	public class IssuedTo
	{

		[XmlElement(ElementName = "Person")]
		public Person Person { get; set; }
	}

	[XmlRoot(ElementName = "PAN")]
	public class PAN
	{

		[XmlAttribute(AttributeName = "verifiedOn")]
		public string VerifiedOn { get; set; }
	}

	[XmlRoot(ElementName = "CertificateData")]
	public class CertificateData
	{

		[XmlElement(ElementName = "PAN")]
		public PAN PAN { get; set; }
	}

	[XmlRoot(ElementName = "CanonicalizationMethod")]
	public class CanonicalizationMethod
	{

		[XmlAttribute(AttributeName = "Algorithm")]
		public string Algorithm { get; set; }
	}

	[XmlRoot(ElementName = "SignatureMethod")]
	public class SignatureMethod
	{

		[XmlAttribute(AttributeName = "Algorithm")]
		public string Algorithm { get; set; }
	}

	[XmlRoot(ElementName = "Transform")]
	public class Transform
	{

		[XmlAttribute(AttributeName = "Algorithm")]
		public string Algorithm { get; set; }
	}

	[XmlRoot(ElementName = "Transforms")]
	public class Transforms
	{

		[XmlElement(ElementName = "Transform")]
		public Transform Transform { get; set; }
	}

	[XmlRoot(ElementName = "DigestMethod")]
	public class DigestMethod
	{

		[XmlAttribute(AttributeName = "Algorithm")]
		public string Algorithm { get; set; }
	}

	[XmlRoot(ElementName = "Reference")]
	public class Reference
	{

		[XmlElement(ElementName = "Transforms")]
		public Transforms Transforms { get; set; }

		[XmlElement(ElementName = "DigestMethod")]
		public DigestMethod DigestMethod { get; set; }

		[XmlElement(ElementName = "DigestValue")]
		public string DigestValue { get; set; }

		[XmlAttribute(AttributeName = "URI")]
		public string URI { get; set; }

		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "SignedInfo")]
	public class SignedInfo
	{

		[XmlElement(ElementName = "CanonicalizationMethod")]
		public CanonicalizationMethod CanonicalizationMethod { get; set; }

		[XmlElement(ElementName = "SignatureMethod")]
		public SignatureMethod SignatureMethod { get; set; }

		[XmlElement(ElementName = "Reference")]
		public Reference Reference { get; set; }
	}

	[XmlRoot(ElementName = "X509Data")]
	public class X509Data
	{

		[XmlElement(ElementName = "X509SubjectName")]
		public string X509SubjectName { get; set; }

		[XmlElement(ElementName = "X509Certificate")]
		public string X509Certificate { get; set; }
	}

	[XmlRoot(ElementName = "KeyInfo")]
	public class KeyInfo
	{

		[XmlElement(ElementName = "X509Data")]
		public X509Data X509Data { get; set; }
	}

	[XmlRoot(ElementName = "Signature")]
	public class Signature
	{

		[XmlElement(ElementName = "SignedInfo")]
		public SignedInfo SignedInfo { get; set; }

		[XmlElement(ElementName = "SignatureValue")]
		public string SignatureValue { get; set; }

		[XmlElement(ElementName = "KeyInfo")]
		public KeyInfo KeyInfo { get; set; }

		[XmlAttribute(AttributeName = "xmlns")]
		public string Xmlns { get; set; }

		[XmlText]
		public string Text { get; set; }
	}

	[XmlRoot(ElementName = "Certificate")]
	public class Certificate
	{

		

		[XmlElement(ElementName = "IssuedTo")]
		public IssuedTo IssuedTo { get; set; }

		[XmlElement(ElementName = "CertificateData")]
		public CertificateData CertificateData { get; set; }

		[XmlElement(ElementName = "Signature")]
		public Signature Signature { get; set; }

		[XmlAttribute(AttributeName = "language")]
		public int Language { get; set; }

		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }

		[XmlAttribute(AttributeName = "number")]
		public string Number { get; set; }

		[XmlAttribute(AttributeName = "issuedAt")]
		public string IssuedAt { get; set; }

		[XmlAttribute(AttributeName = "issueDate")]
		public string IssueDate { get; set; }

		[XmlAttribute(AttributeName = "validFromDate")]
		public string ValidFromDate { get; set; }

		[XmlAttribute(AttributeName = "status")]
		public string Status { get; set; }

		[XmlText]
		public string Text { get; set; }
	}
}
