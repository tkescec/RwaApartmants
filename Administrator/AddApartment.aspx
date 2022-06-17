<%@ Page Title="Novi apartman" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AddApartment.aspx.cs" Inherits="Administrator.AddApartment" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="Head" runat="server">
    
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Titlebar -->
    <div id="titlebar">
        <div class="row">
            <div class="col-md-12">
                <h2>Novi Apartman</h2>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-lg-12">
            <div id="add-listing">
                <!-- Section -->
                <div class="add-listing-section">
                    <!-- Headline -->
                    <div class="add-listing-headline">
                        <h3><i class="sl sl-icon-doc"></i>Osnovne Informacije</h3>
                    </div>

                    <div class="row with-forms">
                        <div class="col-md-12">
                            <h5>Vlasnik (obavezno) <i class="tip" data-tip-content="Odaberite jednog od vlasnika apartmana."></i></h5>
                            <asp:DropDownList ID="ddlOwner" runat="server" CssClass="chosen-select-no-single" AutoPostBack="false" AppendDataBoundItems="true">
                                <asp:ListItem Text="Odaberi vlasnika" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvOwner" meta:resourcekey="rfvOwner" runat="server" ControlToValidate="ddlOwner" Display="Dynamic" ForeColor="#FF9393">Niste odabrali vlasnika.</asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="row with-forms">
                        <div class="col-md-6">
                            <h5>Ime (obavezno) <i class="tip" data-tip-content="Upišite željeno ime apartmana."></i></h5>
                            <asp:TextBox ID="txtApartmentName" class="search-field" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvApartmentName" meta:resourcekey="rfvApartmentName" runat="server" ControlToValidate="txtApartmentName" Display="Dynamic" ForeColor="#FF9393">Niste upisali ime apartmana.</asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <h5>Ime engleski (obavezno) <i class="tip" data-tip-content="Upišite ime apartmana na engleskom jeziku radi lakšeg prevođenja."></i></h5>
                            <asp:TextBox ID="txtApartmentNameEng" class="search-field" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvApartmentNameEng" meta:resourcekey="rfvApartmentNameEng" runat="server" ControlToValidate="txtApartmentNameEng" Display="Dynamic" ForeColor="#FF9393">Niste upisali ime apartmana na engleskom.</asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <!-- Section / End -->

                <!-- Section -->
                <div class="add-listing-section margin-top-45">
                    <!-- Headline -->
                    <div class="add-listing-headline">
                        <h3><i class="sl sl-icon-location"></i>Lokacija</h3>
                    </div>

                    <div class="row with-forms">
                        <div class="col-md-6">
                            <h5>Grad (obavezno) <i class="tip" data-tip-content="Odaberite grad u kojem se nalazi apartman."></i></h5>
                            <asp:DropDownList ID="ddlApartmentCity" runat="server" CssClass="chosen-select-no-single" AutoPostBack="false" AppendDataBoundItems="true">
                                <asp:ListItem Text="Odaberi grad" Value="" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvCity" meta:resourcekey="rfvCity" runat="server" ControlToValidate="ddlApartmentCity" Display="Dynamic" ForeColor="#FF9393">Niste odabrali grad.</asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6">
                            <h5>Adresa <i class="tip" data-tip-content="Upišite adresu apartmana."></i></h5>
                            <asp:TextBox ID="txtApartmentAddress" class="search-field" runat="server"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="rfvApartmentAddress" meta:resourcekey="rfvApartmentAddress" runat="server" ControlToValidate="txtApartmentAddress" Display="Dynamic" ForeColor="#FF9393">Niste upisali ime apartmana.</asp:RequiredFieldValidator>--%>
                        </div>
                    </div>
                </div>
                <!-- Section / End -->

                <!-- Section -->
                <div class="add-listing-section margin-top-45">
                    <!-- Headline -->
                    <div class="add-listing-headline">
                        <h3><i class="sl sl-icon-picture"></i>Galerija slika</h3>
                    </div>
                    <!-- Dropzone -->
                    <div class="file-input-container">
                        <div class="file-input">
                            <asp:FileUpload ID="fileApartmentImages" AllowMultiple="true" runat="server" CssClass="files-input" data-multiple-caption="Broj odabranih slika ({count})" />
                            <label class="label-input text-center" for="fileApartmentImages">
                                <i class="sl sl-icon-plus"></i>
                                <br>
                                <span>Povuci i ispusti fotografije ili odaberi sa računala</span>
                                <br>
                                <span class="files-info text-danger"></span>
                            </label>
                        </div>
                    </div>
                    <div class="images-preview-container row"></div>
                </div>
                <!-- Section / End -->

                <!-- Section -->
                <div class="add-listing-section margin-top-45">
                    <!-- Headline -->
                    <div class="add-listing-headline">
                        <h3><i class="sl sl-icon-docs"></i>Detalji</h3>
                    </div>
                    <div class="row with-forms">
                        <div class="col-md-6">
                            <h5>Broj soba <i class="tip" data-tip-content="Upišite koliko apartman ima soba."></i></h5>
                            <asp:TextBox ID="txtApartmentRooms" class="search-field" runat="server" type="number"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <h5>Broj odraslih osoba <i class="tip" data-tip-content="Upišite koliko odraslih osoba prima apartman."></i></h5>
                            <asp:TextBox ID="txtApartmentAdults" class="search-field" runat="server" type="number"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row with-forms">
                        <div class="col-md-6">
                            <h5>Broj djece <i class="tip" data-tip-content="Upišite koliko djece prima apartman."></i></h5>
                            <asp:TextBox ID="txtApartmentChilds" class="search-field" runat="server" type="number"></asp:TextBox>
                        </div>
                        <div class="col-md-6">
                            <h5>Udaljenost od plaže <i class="tip" data-tip-content="Upišite koliko je apartman udaljen od plaže u metrima."></i></h5>
                            <asp:TextBox ID="txtApartmentBeachDistance" class="search-field" runat="server" type="number"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row with-forms">
                        <div class="col-md-12">
                            <h5>Tagovi <i class="tip" data-tip-content="Odaberite željene tagove za apartman"></i></h5>
                            <div class="checkboxes in-row margin-bottom-20">
                                <asp:CheckBoxList ID="cblApartmentTags" runat="server" AppendDataBoundItems="True" RepeatLayout="UnorderedList"></asp:CheckBoxList>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Section / End -->

                <!-- Section -->
                <div class="add-listing-section margin-top-45">
                    <!-- Headline -->
                    <div class="add-listing-headline">
                        <h3><i class="sl sl-icon-book-open"></i>Cijena</h3>
                    </div>
                    <div class="row with-forms">
                        <div class="col-md-12">
                            <h5>Cijena (obavezno) <i class="tip" data-tip-content="Upišite cijenu apartmana u eurima."></i></h5>
                            <div class="fm-input pricing-price">
                                <asp:TextBox ID="txtApartmentPrice" class="search-field" runat="server" pattern="^\$\d{1,3}(,\d{3})*(\.\d+)?$" type="currency" data-unit="EUR"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvApartmentPrice" meta:resourcekey="rfvApartmentPrice" runat="server" ControlToValidate="txtApartmentPrice" Display="Dynamic" ForeColor="#FF9393">Niste upisali cijenu.</asp:RequiredFieldValidator>
                            </div>

                        </div>
                    </div>
                </div>
                <!-- Section / End -->
            </div>

            <asp:LinkButton ID="btnAddNewApartment" CssClass="button preview" OnClick="btnAddNewApartment_Click" runat="server">Dodaj novi apartman <i class="fa fa-arrow-circle-right"></i></asp:LinkButton>

        </div>
    </div>
</asp:Content>

<asp:Content ID="ScriptContent" ContentPlaceHolderID="Script" runat="server">

    <script>
        (function () {
            $('.dashboard-nav').find('li.active').parent().parent().addClass('active');

            const input = document.querySelector(".files-input");
            const previewContainer = document.querySelector(".images-preview-container");
            const filesInfo = document.querySelector(".files-info");

            const convertBase64 = (file) => {
                return new Promise((resolve, reject) => {
                    const fileReader = new FileReader();
                    fileReader.readAsDataURL(file);

                    fileReader.onload = () => {
                        resolve(fileReader.result);
                    };

                    fileReader.onerror = (error) => {
                        reject(error);
                    };
                });
            };
            const uploadImage = async (event) => {
                const h5 = document.createElement("h5");
                h5.className = "margin-left-15";
                h5.innerText = "Pregled učitanih slika";
                previewContainer.append(h5);

                for (const element of event.target.files) {
                    if (element.type.match('image')) {
                        const base64 = await convertBase64(element);
                        const image = document.createElement("img");
                        image.classList.add("img-thumbnail");
                        image.src = base64;
                        previewContainer.append(image);
                    } else {
                        filesInfo.innerText = "Jedna ili više odabranih datoteka nije slika i neće se uplodati na server."
                    }
                }
            };
            input.addEventListener("change", (e) => {
                previewContainer.innerHTML = "";
                filesInfo.innerText = "";

                if (e.target.files.length > 0) {
                    console.log((e.target.getAttribute('data-multiple-caption') || '').replace('{count}', e.target.files.length));
                    filesInfo.innerText = (e.target.getAttribute('data-multiple-caption') || '').replace('{count}', e.target.files.length);
                    uploadImage(e);
                }
            });

            //Currency input field
            $("input[type='currency']").on({
                keyup: function () {
                    formatCurrency($(this));
                },
                blur: function () {
                    formatCurrency($(this), "blur");
                }
            });
            const formatNumber = (n) => {
                return n.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",")
            }
            const formatCurrency = (input, blur) => {

                let input_val = input.val();

                if (input_val === "") { return; }

                let original_len = input_val.length;
                let caret_pos = input.prop("selectionStart");

                if (input_val.indexOf(".") >= 0) {

                    let decimal_pos = input_val.indexOf(".");
                    let left_side = input_val.substring(0, decimal_pos);
                    let right_side = input_val.substring(decimal_pos);

                    left_side = formatNumber(left_side);
                    right_side = formatNumber(right_side);

                    if (blur === "blur") {
                        right_side += "00";
                    }

                    right_side = right_side.substring(0, 2);
                    input_val = "€" + left_side + "." + right_side;

                } else {

                    input_val = formatNumber(input_val);
                    input_val = "€" + input_val;

                    if (blur === "blur") {
                        input_val += ".00";
                    }
                }

                input.val(input_val);

                let updated_len = input_val.length;
                caret_pos = updated_len - original_len + caret_pos;
                input[0].setSelectionRange(caret_pos, caret_pos);
            }

        })();
    </script>
</asp:Content>
