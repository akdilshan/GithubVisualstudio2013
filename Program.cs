// This code was built using Visual Studio 2005
using System;
using System.Web.Services.Protocols;
using CreatePendingShipmentWebServiceClient.CreatePendingShipmentWebReference;

// Sample code to call the FedEx Create Pending Shipment Web Service
// Tested with Microsoft Visual Studio 2005 Professional Edition
//adding comments to check
namespace CreatePendingShipmentWebServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //creating the request
            CreateOpenShipmentRequest request = BuildCreatePendingShipmentRequest();
            //
            OpenShipService service = new OpenShipService();
			if (usePropertyFile())
            {
                //service.Url = getProperty("endpoint");
                service.Url = getProperty("https://wsbeta.fedex.com:443/web-services");
                
            }
            //
            try
            {
                // Call the web service passing in a CreatePendingShipmentRequest and returning a CreatePendingShipmentReply
                CreateOpenShipmentReply reply = service.createPendingShipment(request);
                if ((reply.HighestSeverity == NotificationSeverityType.SUCCESS) || (reply.HighestSeverity == NotificationSeverityType.NOTE) || (reply.HighestSeverity == NotificationSeverityType.WARNING))
                {
                    ShowCreatePendingShipmentReply(reply);
                }
                ShowNotifications(reply);
            }
            catch (SoapException se)
            {
                Console.WriteLine(se.Detail.InnerText);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("Press any key to quit !");
            Console.ReadKey();
        }

        private static CreateOpenShipmentRequest BuildCreatePendingShipmentRequest()
        {
            //Build the CreatePendingShipmentRequest
            CreateOpenShipmentRequest request = new CreateOpenShipmentRequest();
            request.WebAuthenticationDetail = new WebAuthenticationDetail();
            request.WebAuthenticationDetail.UserCredential = new WebAuthenticationCredential();
            request.WebAuthenticationDetail.UserCredential.Key = "z3Zhp3P6JUxEWTnY"; // Replace "XXX" with the Key
            request.WebAuthenticationDetail.UserCredential.Password = "Ws0vZF6hlpT6XF2uT119RToXD"; // Replace "XXX" with the Password
            request.WebAuthenticationDetail.ParentCredential = new WebAuthenticationCredential();
            request.WebAuthenticationDetail.ParentCredential.Key = "z3Zhp3P6JUxEWTnY"; // Replace "XXX" with the Key
            request.WebAuthenticationDetail.ParentCredential.Password = "Ws0vZF6hlpT6XF2uT119RToXD"; // Replace "XXX"
            //if (usePropertyFile()) //Set values from a file for testing purposes
            //{
            //    request.WebAuthenticationDetail.UserCredential.Key = getProperty("z3Zhp3P6JUxEWTnY");
            //    request.WebAuthenticationDetail.UserCredential.Password = getProperty("sZEPcRsdYLufMgHcyiQLWBFKy");
            //    request.WebAuthenticationDetail.ParentCredential.Key = getProperty("z3Zhp3P6JUxEWTnY");
            //    request.WebAuthenticationDetail.ParentCredential.Password = getProperty("sZEPcRsdYLufMgHcyiQLWBFKy");
            //}
            //
            request.ClientDetail = new ClientDetail();

            request.ClientDetail.AccountNumber = "510087208"; // Replace "XXX" with the client's account number
            request.ClientDetail.MeterNumber = "118704008"; // Replace "XXX" with the client's meter number
            //if (usePropertyFile()) //Set values from a file for testing purposes
            //{
            //    request.ClientDetail.AccountNumber = getProperty("510087208");
            //    request.ClientDetail.MeterNumber = getProperty("118704008");
            //}
            //
            request.TransactionDetail = new TransactionDetail();
            request.TransactionDetail.CustomerTransactionId = "***Create Pending Shipment Request using VC#***"; //The client will get the same value back in the reply
            //
            request.Version = new VersionId();
            //
            SetShipmentDetails(request);
            //
            request.Actions = new CreateOpenShipmentActionType[1] { CreateOpenShipmentActionType.TRANSFER };
            //
            return request;
        }

        private static void SetShipmentDetails(CreateOpenShipmentRequest request)
        {
            request.RequestedShipment = new RequestedShipment();
            request.RequestedShipment.ShipTimestamp = DateTime.Now; // Ship date and time
            request.RequestedShipment.DropoffType = DropoffType.REGULAR_PICKUP;
            request.RequestedShipment.DropoffTypeSpecified = true;
            request.RequestedShipment.ServiceType = ServiceType.PRIORITY_OVERNIGHT; // Service types are STANDARD_OVERNIGHT, PRIORITY_OVERNIGHT, FEDEX_GROUND ...
            request.RequestedShipment.ServiceTypeSpecified = true;
            request.RequestedShipment.PackagingType = PackagingType.YOUR_PACKAGING; // Packaging type FEDEX_BOX, FEDEX_PAK, FEDEX_TUBE, YOUR_PACKAGING, ...
            request.RequestedShipment.PackagingTypeSpecified = true;
            //
            request.RequestedShipment.TotalWeight = new Weight(); // Total weight information
            request.RequestedShipment.TotalWeight.Value = 50.0M;
            request.RequestedShipment.TotalWeight.ValueSpecified = true;
            request.RequestedShipment.TotalWeight.Units = WeightUnits.LB;
            request.RequestedShipment.TotalWeight.UnitsSpecified = true;
            //
            request.RequestedShipment.PackageCount = "1";
            //
            SetSender(request);
            //
            SetRecipient(request);
            //
            SetPayment(request);
            //
            SetLabelDetails(request);
            //
            SetPackageLineItems(request);
            //
            bool isCodShipment = false; // Set this to true to process a COD shipment
            //
            SetSpecialServices(request, isCodShipment);
        }

        private static void SetSender(CreateOpenShipmentRequest request)
        {
            request.RequestedShipment.Shipper = new Party();
            request.RequestedShipment.Shipper.Contact = new Contact();
            request.RequestedShipment.Shipper.Contact.PersonName = "Sender Name";
            request.RequestedShipment.Shipper.Contact.CompanyName = "Sender Company Name";
            request.RequestedShipment.Shipper.Contact.PhoneNumber = "0805522713";
            //
            request.RequestedShipment.Shipper.Address = new Address();
            request.RequestedShipment.Shipper.Address.StreetLines = new string[1] { "1202 Chalet Ln" };
            request.RequestedShipment.Shipper.Address.City = "Harrison";
            request.RequestedShipment.Shipper.Address.StateOrProvinceCode = "AR";
            request.RequestedShipment.Shipper.Address.PostalCode = "72601";
            request.RequestedShipment.Shipper.Address.CountryCode = "US";
        }

        private static void SetRecipient(CreateOpenShipmentRequest request)
        {
            request.RequestedShipment.Recipient = new Party();
            request.RequestedShipment.Recipient.Contact = new Contact();
            request.RequestedShipment.Recipient.Contact.PersonName = "Recipient Name";
            request.RequestedShipment.Recipient.Contact.CompanyName = "Recipient Company Name";
            request.RequestedShipment.Recipient.Contact.PhoneNumber = "9012637906";
            //
            request.RequestedShipment.Recipient.Address = new Address();
            request.RequestedShipment.Recipient.Address.StreetLines = new string[1] { "Address Line 1" };
            request.RequestedShipment.Recipient.Address.City = "Windsor";
            request.RequestedShipment.Recipient.Address.StateOrProvinceCode = "CT";
            request.RequestedShipment.Recipient.Address.PostalCode = "06006";
            request.RequestedShipment.Recipient.Address.CountryCode = "US";
            request.RequestedShipment.Recipient.Address.Residential = true;
            request.RequestedShipment.Recipient.Address.ResidentialSpecified = true;
        }

        private static void SetPayment(CreateOpenShipmentRequest request)
        {
            request.RequestedShipment.ShippingChargesPayment = new Payment();
            request.RequestedShipment.ShippingChargesPayment.PaymentType = PaymentType.SENDER;
            request.RequestedShipment.ShippingChargesPayment.PaymentTypeSpecified = true;
            request.RequestedShipment.ShippingChargesPayment.Payor = new Payor();
            request.RequestedShipment.ShippingChargesPayment.Payor.ResponsibleParty = new Party();
            request.RequestedShipment.ShippingChargesPayment.Payor.ResponsibleParty.AccountNumber = "XXX"; // Replace "XXX" with client's account number
            if (usePropertyFile()) //Set values from a file for testing purposes
            {
                request.RequestedShipment.ShippingChargesPayment.Payor.ResponsibleParty.AccountNumber = getProperty("payoraccount");
            }
            request.RequestedShipment.ShippingChargesPayment.Payor.ResponsibleParty.Contact = new Contact();
            request.RequestedShipment.ShippingChargesPayment.Payor.ResponsibleParty.Address = new Address();
            request.RequestedShipment.ShippingChargesPayment.Payor.ResponsibleParty.Address.CountryCode = "US";
        }

        private static void SetLabelDetails(CreateOpenShipmentRequest request)
        {
            request.RequestedShipment.LabelSpecification = new LabelSpecification();
            request.RequestedShipment.LabelSpecification.ImageType = ShippingDocumentImageType.PDF; // Use this line for a PDF label
            request.RequestedShipment.LabelSpecification.ImageTypeSpecified = true;
            request.RequestedShipment.LabelSpecification.LabelFormatType = LabelFormatType.COMMON2D;
            request.RequestedShipment.LabelSpecification.LabelFormatTypeSpecified = true;
            request.RequestedShipment.LabelSpecification.LabelStockType = LabelStockType.STOCK_4X6;
            request.RequestedShipment.LabelSpecification.LabelStockTypeSpecified = true;
            request.RequestedShipment.LabelSpecification.LabelPrintingOrientation = LabelPrintingOrientationType.BOTTOM_EDGE_OF_TEXT_FIRST;
            request.RequestedShipment.LabelSpecification.LabelPrintingOrientationSpecified = true;
        }

        private static void SetPackageLineItems(CreateOpenShipmentRequest request)
        {
            request.RequestedShipment.RequestedPackageLineItems = new RequestedPackageLineItem[1];
            request.RequestedShipment.RequestedPackageLineItems[0] = new RequestedPackageLineItem();
            request.RequestedShipment.RequestedPackageLineItems[0].SequenceNumber = "1";
            request.RequestedShipment.RequestedPackageLineItems[0].Weight = new Weight(); // Package weight information
            request.RequestedShipment.RequestedPackageLineItems[0].Weight.Value = 50.0M;
            request.RequestedShipment.RequestedPackageLineItems[0].Weight.ValueSpecified = true;
            request.RequestedShipment.RequestedPackageLineItems[0].Weight.Units = WeightUnits.LB;
            request.RequestedShipment.RequestedPackageLineItems[0].Weight.UnitsSpecified = true;
            request.RequestedShipment.RequestedPackageLineItems[0].ItemDescription = "test";
        }

        private static void SetSpecialServices(CreateOpenShipmentRequest request, bool isCodShipment)
        {
            request.RequestedShipment.SpecialServicesRequested = new ShipmentSpecialServicesRequested();
            if (isCodShipment)
            {
                SetCOD(request);
            }
            else
            {
                request.RequestedShipment.SpecialServicesRequested.SpecialServiceTypes = new ShipmentSpecialServiceType[2] { new ShipmentSpecialServiceType(), new ShipmentSpecialServiceType() };
                request.RequestedShipment.SpecialServicesRequested.SpecialServiceTypes[0] = ShipmentSpecialServiceType.RETURN_SHIPMENT;
                request.RequestedShipment.SpecialServicesRequested.SpecialServiceTypes[1] = ShipmentSpecialServiceType.PENDING_SHIPMENT;
                SetReturnShipmentDetails(request);
            }
            //
            SetPendingShipmentDetails(request);
            //
            SetEmailNotificationDetails(request);
        }

        private static void SetReturnShipmentDetails(CreateOpenShipmentRequest request)
        {
            request.RequestedShipment.SpecialServicesRequested.ReturnShipmentDetail = new ReturnShipmentDetail();
            request.RequestedShipment.SpecialServicesRequested.ReturnShipmentDetail.ReturnType = ReturnType.PENDING;
            request.RequestedShipment.SpecialServicesRequested.ReturnShipmentDetail.ReturnTypeSpecified = true;
            request.RequestedShipment.SpecialServicesRequested.ReturnShipmentDetail.Rma = new Rma();
            request.RequestedShipment.SpecialServicesRequested.ReturnShipmentDetail.Rma.Reason = "Test RMA# value ";
            request.RequestedShipment.SpecialServicesRequested.ReturnShipmentDetail.ReturnEMailDetail = new ReturnEMailDetail();
            request.RequestedShipment.SpecialServicesRequested.ReturnShipmentDetail.ReturnEMailDetail.MerchantPhoneNumber = "901-123-4567";
        }

        private static void SetPendingShipmentDetails(CreateOpenShipmentRequest request)
        {
            request.RequestedShipment.SpecialServicesRequested.PendingShipmentDetail = new PendingShipmentDetail();
            request.RequestedShipment.SpecialServicesRequested.PendingShipmentDetail.Type = PendingShipmentType.EMAIL;
            request.RequestedShipment.SpecialServicesRequested.PendingShipmentDetail.TypeSpecified = true;
            request.RequestedShipment.SpecialServicesRequested.PendingShipmentDetail.ExpirationDate = DateTime.Now.AddDays(30);
            request.RequestedShipment.SpecialServicesRequested.PendingShipmentDetail.ExpirationDateSpecified = true;
            request.RequestedShipment.SpecialServicesRequested.PendingShipmentDetail.EmailLabelDetail = new EMailLabelDetail();
            request.RequestedShipment.SpecialServicesRequested.PendingShipmentDetail.EmailLabelDetail.Recipients = new EMailRecipient[1] { new EMailRecipient() };
            request.RequestedShipment.SpecialServicesRequested.PendingShipmentDetail.EmailLabelDetail.Recipients[0].EmailAddress = "test@test.com";
            request.RequestedShipment.SpecialServicesRequested.PendingShipmentDetail.EmailLabelDetail.Recipients[0].Role = AccessorRoleType.SHIPMENT_COMPLETOR;
            request.RequestedShipment.SpecialServicesRequested.PendingShipmentDetail.EmailLabelDetail.Recipients[0].RoleSpecified = true;
            request.RequestedShipment.SpecialServicesRequested.PendingShipmentDetail.EmailLabelDetail.Message = "Test notification";
        }

        private static void SetEmailNotificationDetails(CreateOpenShipmentRequest request)
        {
            request.RequestedShipment.SpecialServicesRequested.EMailNotificationDetail = new EMailNotificationDetail();
            request.RequestedShipment.SpecialServicesRequested.EMailNotificationDetail.PersonalMessage = "test message";
            request.RequestedShipment.SpecialServicesRequested.EMailNotificationDetail.Recipients = new EMailNotificationRecipient[1] { new EMailNotificationRecipient() };
            request.RequestedShipment.SpecialServicesRequested.EMailNotificationDetail.Recipients[0].EMailAddress = "test@test.com";
            request.RequestedShipment.SpecialServicesRequested.EMailNotificationDetail.Recipients[0].EMailNotificationRecipientType = EMailNotificationRecipientType.RECIPIENT;
            request.RequestedShipment.SpecialServicesRequested.EMailNotificationDetail.Recipients[0].EMailNotificationRecipientTypeSpecified = true;
            request.RequestedShipment.SpecialServicesRequested.EMailNotificationDetail.Recipients[0].NotificationEventsRequested = new EMailNotificationEventType[1] { new EMailNotificationEventType() };
            request.RequestedShipment.SpecialServicesRequested.EMailNotificationDetail.Recipients[0].NotificationEventsRequested[0] = EMailNotificationEventType.ON_DELIVERY;
            request.RequestedShipment.SpecialServicesRequested.EMailNotificationDetail.Recipients[0].Localization = new Localization();
            request.RequestedShipment.SpecialServicesRequested.EMailNotificationDetail.Recipients[0].Localization.LanguageCode = "en";
            request.RequestedShipment.SpecialServicesRequested.EMailNotificationDetail.Recipients[0].Localization.LocaleCode = "US";
        }

        private static void SetCOD(CreateOpenShipmentRequest request)
        {
            // Set COD at Package level for Ground Services
            if (request.RequestedShipment.ServiceType == ServiceType.FEDEX_GROUND || request.RequestedShipment.ServiceType == ServiceType.GROUND_HOME_DELIVERY)
            {
                request.RequestedShipment.SpecialServicesRequested.SpecialServiceTypes = new ShipmentSpecialServiceType[1] { new ShipmentSpecialServiceType() };
                request.RequestedShipment.SpecialServicesRequested.SpecialServiceTypes[0] = ShipmentSpecialServiceType.PENDING_SHIPMENT;
                //
                request.RequestedShipment.RequestedPackageLineItems[0].CustomerReferences = new CustomerReference[1];
                request.RequestedShipment.RequestedPackageLineItems[0].CustomerReferences[0] = new CustomerReference();
                request.RequestedShipment.RequestedPackageLineItems[0].CustomerReferences[0].CustomerReferenceType = new CustomerReferenceType();
                request.RequestedShipment.RequestedPackageLineItems[0].CustomerReferences[0].CustomerReferenceType = CustomerReferenceType.RMA_ASSOCIATION;
                request.RequestedShipment.RequestedPackageLineItems[0].CustomerReferences[0].CustomerReferenceTypeSpecified = true;
                request.RequestedShipment.RequestedPackageLineItems[0].CustomerReferences[0].Value = "RMA_value";
                //
                request.RequestedShipment.RequestedPackageLineItems[0].SpecialServicesRequested = new PackageSpecialServicesRequested();
                request.RequestedShipment.RequestedPackageLineItems[0].SpecialServicesRequested.SpecialServiceTypes = new PackageSpecialServiceType[1]{ new PackageSpecialServiceType()};
                request.RequestedShipment.RequestedPackageLineItems[0].SpecialServicesRequested.SpecialServiceTypes[0] = PackageSpecialServiceType.COD;
                //
                request.RequestedShipment.RequestedPackageLineItems[0].SpecialServicesRequested.CodDetail = new CodDetail();
                request.RequestedShipment.RequestedPackageLineItems[0].SpecialServicesRequested.CodDetail.CollectionType = CodCollectionType.GUARANTEED_FUNDS;
                request.RequestedShipment.RequestedPackageLineItems[0].SpecialServicesRequested.CodDetail.CollectionTypeSpecified = true;
                request.RequestedShipment.RequestedPackageLineItems[0].SpecialServicesRequested.CodDetail.CodCollectionAmount = new Money();
                request.RequestedShipment.RequestedPackageLineItems[0].SpecialServicesRequested.CodDetail.CodCollectionAmount.Amount = 250.00M;
                request.RequestedShipment.RequestedPackageLineItems[0].SpecialServicesRequested.CodDetail.CodCollectionAmount.AmountSpecified = true;
                request.RequestedShipment.RequestedPackageLineItems[0].SpecialServicesRequested.CodDetail.CodCollectionAmount.Currency = "USD";
            }
            else
            {
                // Set COD at Shipment level for Express Services
                request.RequestedShipment.SpecialServicesRequested.SpecialServiceTypes = new ShipmentSpecialServiceType[2] { new ShipmentSpecialServiceType(), new ShipmentSpecialServiceType() };
                request.RequestedShipment.SpecialServicesRequested.SpecialServiceTypes[0] = ShipmentSpecialServiceType.PENDING_SHIPMENT;
                request.RequestedShipment.SpecialServicesRequested.SpecialServiceTypes[1] = ShipmentSpecialServiceType.COD;
                //
                request.RequestedShipment.SpecialServicesRequested.CodDetail = new CodDetail();
                request.RequestedShipment.SpecialServicesRequested.CodDetail.CollectionType = CodCollectionType.GUARANTEED_FUNDS;
                request.RequestedShipment.SpecialServicesRequested.CodDetail.CodCollectionAmount = new Money();
                request.RequestedShipment.SpecialServicesRequested.CodDetail.CodCollectionAmount.Amount = 250.00M;
                request.RequestedShipment.SpecialServicesRequested.CodDetail.CodCollectionAmount.AmountSpecified = true;
                request.RequestedShipment.SpecialServicesRequested.CodDetail.CodCollectionAmount.Currency = "USD";
            }
        }

        private static void ShowCreatePendingShipmentReply(CreateOpenShipmentReply reply)
        {
            Console.WriteLine("CreatePendingShipmentReply details:");
            Console.WriteLine("Customer Transaction ID : " + reply.TransactionDetail.CustomerTransactionId);
            Console.WriteLine("Completed Shipment Details:");
            Console.WriteLine("**************************************************************************");
            Console.WriteLine("Url : " + reply.CompletedShipmentDetail.AccessDetail[0].EmailLabelUrl);
            Console.WriteLine("User Id : " + reply.CompletedShipmentDetail.AccessDetail[0].UserId);
            Console.WriteLine("Password : " + reply.CompletedShipmentDetail.AccessDetail[0].Password);
            if (reply.CompletedShipmentDetail.AccessDetail[0].RoleSpecified)
            {
                Console.WriteLine("Role : " + reply.CompletedShipmentDetail.AccessDetail[0].Role);
            }
            Console.WriteLine("Service Type Description : " + reply.CompletedShipmentDetail.ServiceTypeDescription);
            Console.WriteLine("Packaging Description  : " + reply.CompletedShipmentDetail.PackagingDescription);
            Console.WriteLine("Package Details:");
            Console.WriteLine("-------------------------------------------------------------------");

            for (int i = 0; i < reply.CompletedShipmentDetail.CompletedPackageDetails.Length; i++)
            {
                CompletedPackageDetail packageDetail = reply.CompletedShipmentDetail.CompletedPackageDetails[i];
                Console.WriteLine("Tracking Number : " + packageDetail.TrackingIds[i].TrackingNumber);
                Console.WriteLine("Form Id : " + packageDetail.TrackingIds[i].FormId);
                Console.WriteLine("Signature Option : {0}", packageDetail.SignatureOption);
                Console.WriteLine("Sequence Number : {0}", packageDetail.SequenceNumber);
                Console.WriteLine("***********************************");
            }
            Console.WriteLine("**************************************************************************");
        }

        private static void ShowNotifications(CreateOpenShipmentReply reply)
        {
            Console.WriteLine("Notifications");
            for (int i = 0; i < reply.Notifications.Length; i++)
            {
                Notification notification = reply.Notifications[i];
                Console.WriteLine("Notification no. {0}", i);
                Console.WriteLine(" Severity: {0}", notification.Severity);
                Console.WriteLine(" Code: {0}", notification.Code);
                Console.WriteLine(" Message: {0}", notification.Message);
                Console.WriteLine(" Source: {0}", notification.Source);
            }
        }
        private static bool usePropertyFile() //Set to true for common properties to be set with getProperty function.
        {
            return getProperty("usefile").Equals("True");
        }
        private static String getProperty(String propertyname) //Sets common properties for testing purposes.
        {
            try
            {
                String filename = "C:\\filepath\\filename.txt";
                if (System.IO.File.Exists(filename))
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(filename);
                    do
                    {
                        String[] parts = sr.ReadLine().Split(',');
                        if (parts[0].Equals(propertyname) && parts.Length == 2)
                        {
                            return parts[1];
                        }
                    }
                    while (!sr.EndOfStream);
                }
                Console.WriteLine("Property {0} set to default 'XXX'", propertyname);
                return "XXX";
            }
            catch (Exception e)
            {
                Console.WriteLine("Property {0} set to default 'XXX'", propertyname);
                return "XXX";
            }
        }
    }
}
